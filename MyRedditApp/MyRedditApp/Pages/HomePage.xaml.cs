using System;
using System.Collections.Generic;
using MyRedditApp.ViewModels;
using Xamarin.Forms;

namespace MyRedditApp.Pages
{
    public partial class HomePage : ContentPage
    {
        #region Constructor

        public HomePage()
        {
            BindingContext = new HomePageVM();

            InitializeComponent();
        }

        #endregion
    }
}
