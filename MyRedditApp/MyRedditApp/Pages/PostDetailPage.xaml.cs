using Xamarin.Forms;

using MyRedditApp.Models;
using MyRedditApp.ViewModels;

namespace MyRedditApp.Pages
{
    public partial class PostDetailPage : ContentPage
    {
        #region Constructor
        public PostDetailPage(Post selected = null)
        {
            var viewModel = new PostDetailPageVM(selected);
            BindingContext = viewModel;

            InitializeComponent();
        }
        #endregion

        #region Override methods

        /// <summary>
        /// Ons the appearing.
        /// </summary>
        protected override void OnAppearing()
        {
            base.OnAppearing();

            MessagingCenter.Subscribe<PostDetailPageVM>(this, AppConfig.ImageSaved, (sender) =>
            {
                DisplayAlert("Success", "The Image is now in your Gallery!", "Ok");
            });

        }
		#endregion
    }
}
