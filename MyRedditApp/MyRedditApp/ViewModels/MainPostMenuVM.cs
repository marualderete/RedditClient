using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

using Xamarin.Forms;

using MyRedditApp.Helpers;
using MyRedditApp.Models;
using MyRedditApp.Pages;
using MyRedditApp.Services.Interfaces;
using MyRedditApp.Services;

namespace MyRedditApp.ViewModels
{
    public class MainPostMenuVM : BaseVM
    {
        #region Private Properties

        bool _isRefreshing;
        string _errorMessage;

        Command<Post> _postSelectedCommand;
        Command<String> _dismissCommand;
        Command _dismissAllCommand;
        Command _refreshCommand;

        IPostService _postService;
        IAuthenticationService _authService;

        ObservableCollection<Post> _posts;
        PostStoreModel _currentPostStore;

        string _after = string.Empty;

        #endregion

        #region Constructor

        public MainPostMenuVM()
        {
            IsRefreshing = false;
            ErrorMessage = string.Empty;

            _postService = ServiceLocator.Instance.Get<IPostService>();
            _authService = ServiceLocator.Instance.Get<IAuthenticationService>();

            GetNewAccessToken();
        }

        #endregion

        #region Public properties

        public bool IsRefreshing
        {
            get { return _isRefreshing; }
            set { SetProperty(ref _isRefreshing, value); }
        }

        public string ErrorMessage
        {
            get { return _errorMessage; }
            set { SetProperty(ref _errorMessage, value); }
        }

        public ObservableCollection<Post> Posts
        {
            get { return _posts; }
            set { SetProperty(ref _posts, value); }
        }

        public PostStoreModel CurrentPostStore
        {
            get { return _currentPostStore; }
            set { SetProperty(ref _currentPostStore, value); }
        }
        #endregion

        #region Commands

        /// <summary>
        /// Pull to refresh command action
        /// </summary>
        /// <value>The refresh command.</value>
        public Command RefreshCommand
        {
            get
            {
                if (_refreshCommand == null)
                {
                    _refreshCommand = new Command( () =>
                    {
                        IsRefreshing = true;
                        LoadPostsAsync(null);
    					IsRefreshing = false;

    
                    });
                }
                return _refreshCommand;
            }
        }

        /// <summary>
        /// Navigates to Post Detail Page passing the tapped Post as parameter
        /// </summary>
        /// <value>The post selected command.</value>
        public Command<Post> PostSelectedCommand
        {
            get
            {
                if (_postSelectedCommand == null)
                {
                    _postSelectedCommand = new Command<Post>(async postSelected =>
                   {
                       await Application.Current.MainPage.Navigation.PushAsync(new PostDetailPage(postSelected));
                   });
                }
                return _postSelectedCommand;
            }
        }

        /// <summary>
        /// Dismiss the current tapped post
        /// </summary>
        /// <value>The dismiss command.</value>
        public Command<String> DismissCommand
        {
            get
            {
                if (_dismissCommand == null)
                {
                    _dismissCommand = new Command<String>(async fullnameId =>
                    {

                        await _postService.DismissPost(fullnameId);
                        var postToRemove = Posts.Where(x => x.PostDetail.FullName == fullnameId).FirstOrDefault();
                        try
                        {
							Posts.Remove(postToRemove);

                        }catch(Exception e)
                        {
                            throw new Exception("Error Dismissing post", e);
                        }
                    });
                }
                return _dismissCommand;
            }
        
        }

        /// <summary>
        /// Dismiss all posts shown
        /// </summary>
        /// <value>The dismiss all command.</value>
        public Command DismissAllCommand
        {
            get
            {
                if (_dismissAllCommand == null)
                {
                    _dismissAllCommand = new Command(async () =>
                    {

                        await _postService.DismissAll(Posts.ToList());
                        Posts.Clear();

                    });
                }
                return _dismissAllCommand;
            }

        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Calls Reddit API to brings first 5 top posts.
        /// </summary>
        /// <returns>The posts async.</returns>
        async void GetNewAccessToken()
        {
            try
            {
                var isDone= await _authService.GetRequestToken();

                if (isDone)
                {
					//Posts = new ObservableCollection<Post>();

                    LoadPostsAsync();

                }

            }
            catch (Exception e)
            {
                throw new Exception("Error getting a new token ", e);
            }
        }

        /// <summary>
        /// Calls Reddit API to brings first 5 top posts.
        /// </summary>
        /// <returns>The posts async.</returns>
        async void LoadPostsAsync(string pAfter = null, bool enableRefreshSpinner = true, bool firstExecution = false)
        {
            try
            {
                IsRefreshing = enableRefreshSpinner;

                CurrentPostStore = await _postService.GetItemAsync(AppConfig.TopPost, pAfter);

                if(string.IsNullOrEmpty(pAfter))
                {
                    //This mean the user wants to load more posts, this is not a refresh.
                    Posts = new ObservableCollection<Post>();
                }
				
                foreach(Post eachPost in CurrentPostStore.PostChildren.OfType<Post>())
                {
                    eachPost.UpdateDatesAgoString();
                    Posts.Add(eachPost);
                }
				
                IsRefreshing = false;
				_after = CurrentPostStore.After;

            }
            catch (Exception e)
            {
                throw new Exception("Error loading posts", e);
            }
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Loads the more posts.This method is calling i
        /// </summary>
        /// <returns>The more posts.</returns>
        /// <param name="after">After.</param>
        public async Task<bool> LoadMorePosts(string after)
        {
            try{

                LoadPostsAsync(after, false);
                //Task more = new Task(async () => await LoadPostsAsync(after, false));
                //more.Wait();
                
            }catch(Exception e)
            {
                throw new Exception("Sorry! There are some errors trying to load more posts.", e);
            }
            return true;

        }

        #endregion
    }
}
