using System;

using Xamarin.Forms;

namespace MyRedditApp.ViewModels
{
    public class HomePageVM : BaseVM
    {
        #region Private Properties

        bool _showError;
        string _errorMessage;

        #endregion

        #region Constructor
        public HomePageVM()
        {
            ShowError = false;
            ErrorMessage = string.Empty;

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

        public ImageSource UserProfilePhoto
        {
            get { return ImageSource.FromFile("redditprofile.jpg"); }
        }

        #endregion


    }
}
