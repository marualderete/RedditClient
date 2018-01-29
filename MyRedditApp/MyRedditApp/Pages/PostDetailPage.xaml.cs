using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyRedditApp.Models;
using MyRedditApp.ViewModels;
using Xamarin.Forms;

namespace MyRedditApp.Pages
{
    public partial class PostDetailPage : ContentPage
    {
        public PostDetailPage(Post selected)
        {
            var viewModel = new PostDetailPageVM(selected);
            BindingContext = viewModel;

            InitializeComponent();

           
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            MessagingCenter.Subscribe<PostDetailPageVM>(this, AppConfig.ImageSaved, (sender) =>
            {
                DisplayAlert("Success", "The Image is now in your Gallery!", "Ok");
            });

        }
    }
}
