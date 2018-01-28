using System;
using System.Collections.Generic;
using System.Linq;
using MyRedditApp.Models;
using MyRedditApp.ViewModels;
using Xamarin.Forms;

namespace MyRedditApp.Pages
{

    public partial class MainPostMenu : MasterDetailPage
    {
        #region Constructor

        public MainPostMenu()
        {
            BindingContext = new MainPostMenuVM();

            // Set the default page, this is the "home" page.
            Detail = new NavigationPage(new HomePage());

            InitializeComponent();
        }

        #endregion

        #region Events

        /// <summary>
        /// Prizes the item appearing. Load by demand in list view when the user scroll to the bottom
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">E.</param>
        async void PostAppearing(object sender, ItemVisibilityEventArgs e)
        {
            var viewCellDetail = e.Item as Post;
			var viewModel = BindingContext as MainPostMenuVM;

            if (viewModel.Posts.Last() == viewCellDetail)
            {
                await viewModel.LoadMorePosts(viewModel.CurrentPostStore.After);
            }

          

        }

        #endregion
    }
}
