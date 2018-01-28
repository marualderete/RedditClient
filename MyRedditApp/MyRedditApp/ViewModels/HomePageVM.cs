using System;

using Xamarin.Forms;

using MyRedditApp.Helpers;
using MyRedditApp.Services.Interfaces;

namespace MyRedditApp.ViewModels
{
    public class HomePageVM : BaseVM
    {
        #region Private Properties

        bool _showError;
        string _errorMessage;


        Command _getTopPostsCommand;
        IPostService _postService;

        #endregion

        #region Constructor
        public HomePageVM()
        {
            ShowError = false;
            ErrorMessage = string.Empty;

            _postService = ServiceLocator.Instance.Get<IPostService>();

        }

        #endregion

        #region Public properties

        public bool ShowError
        {
            get { return _showError; }
            set { SetProperty(ref _showError, value); }
        }

        public string ErrorMessage
        {
            get { return _errorMessage; }
            set { SetProperty(ref _errorMessage, value); }
        }

        #endregion

        #region Commands

        public Command GetTopPostsCommand
        {
            get
            {
                if (_getTopPostsCommand == null)
                {
                    _getTopPostsCommand = new Command(async () =>
                    {
                        try{
                            
                            var topModelStore = await _postService.GetItemAsync(AppConfig.TopPost);

                            var children = topModelStore.PostChildren;

                            
                        }catch(Exception e)
                        {
                            throw new Exception("Error", e);
                        }
                
                       // Application.Current.MainPage.Navigation.PushPopupAsync(new MenuPage());
                    });
                }
                return _getTopPostsCommand;
            }
        }

        #endregion


    }
}
