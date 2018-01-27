using System;
using MyRedditApp.Helpers;

namespace MyRedditApp.ViewModels
{
    public class BaseViewModel : ObservableObject
    {
        #region Privates Properties

        protected string _titlePage;
        protected bool _isBusy = false;

        #endregion

        #region Public Properties

        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                if (value != _isBusy)
                {
                    _isBusy = value;
                    OnPropertyChanged("IsBusy");
                }
            }
        }

        public string TitlePage
        {
            get { return _titlePage; }
            set
            {
                if (value != _titlePage)
                {
                    _titlePage = value;
                    OnPropertyChanged("TitlePage");
                }
            }
        }

        #endregion 
    }
}
