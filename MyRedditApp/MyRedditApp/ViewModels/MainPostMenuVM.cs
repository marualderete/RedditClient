using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using MyRedditApp.Helpers;
using MyRedditApp.Models;
using MyRedditApp.Pages;
using MyRedditApp.Services.Interfaces;
using Xamarin.Forms;

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

        ObservableCollection<Post> _posts;
        PostStoreModel _currentPostStore;

        string _before = string.Empty;
        string _after = string.Empty;

        #endregion

        #region Constructor

        public MainPostMenuVM()
        {
            IsRefreshing = false;
            ErrorMessage = string.Empty;

            _postService = ServiceLocator.Instance.Get<IPostService>();
            LoadPostsAsync();
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

        public Command RefreshCommand
        {
            get
            {
                if (_refreshCommand == null)
                {
                    _refreshCommand = new Command( async () =>
                    {
                        IsRefreshing = true;
                        await LoadPostsAsync(null).ContinueWith( result => {
                            
							IsRefreshing = false;

                        });

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

        public Command<String> DismissCommand
        {
            get
            {
                if (_dismissCommand == null)
                {
                    _dismissCommand = new Command<String>(async fullnameId =>
                    {

                        await _postService.DismissPost(fullnameId);
                        Posts.Remove(Posts.Where(x => x.PostDetail.FullName == fullnameId).Single());
                     //   await Application.Current.MainPage.Navigation.PushAsync(new PostDetailPage(postSelected));
                    });
                }
                return _dismissCommand;
            }
        
        }

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
        async Task<bool> LoadPostsAsync(string pAfter = null)
        {
            try
            {
                IsRefreshing = true;

                CurrentPostStore = await _postService.GetItemAsync(AppConfig.TopPost, pAfter);

				if(pAfter != null) 
				{
					//This mean the user wants to load more posts, this is not a refresh.
                    foreach(Post eachPost in CurrentPostStore.PostChildren.OfType<Post>())
                    {
                        Posts.Add(eachPost);
                    }
				}
				else
				{
					//This mean the user wants reload and see the newest post, so we should refresh the list.
                    Posts = new ObservableCollection<Post>(CurrentPostStore.PostChildren.OfType<Post>());

				}
                IsRefreshing = false;
				_after = CurrentPostStore.After;


                return true;
            }
            catch (Exception e)
            {
                throw new Exception("Error", e);
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
                
				this.LoadPostsAsync(after);
                
            }catch(Exception e)
            {
                throw new Exception("Sorry! There are some errors trying to load more posts.", e);
            }
            return true;
        }

        #endregion
    }

    public class MainMenuItem
    {
        public string Title { get; set; }
    }
}
