using System;
using System.IO;
using MyRedditApp.Helpers;
using MyRedditApp.Models;
using MyRedditApp.PlatformServices;
using MyRedditApp.Services.Interfaces;
using Xamarin.Forms;

namespace MyRedditApp.ViewModels
{
    public class PostDetailPageVM : BaseVM
    {
        #region Private Properties

        bool _isRefreshing;
        string _errorMessage;

        Command _saveImgToGalleryCommand;

        IPostService _postService;
        IMediaService _mediaService;

        Post _currentPost;

        #endregion

        #region Constructor
        public PostDetailPageVM(Post selected)
        {
            IsRefreshing = false;
            ErrorMessage = string.Empty;
            CurrentPost = selected;
            _postService = ServiceLocator.Instance.Get<IPostService>();
            _mediaService = DependencyService.Get<IMediaService>();
        }
        #endregion


        #region Public methods

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

        public string AuthorString
        {
            get { return "Posted by " + _currentPost.PostDetail.Author; }
        }

        public ImageSource PostImageToShow
        {
            get { return _currentPost.GetPostImage(); }
        }

        public Post CurrentPost
        {
            get { return _currentPost; }
            set { SetProperty(ref _currentPost, value); }
        }

        public ImageSource UserProfilePhoto
        {
            get { return ImageSource.FromFile("redditprofile.jpg"); }
        }

        #endregion

        #region Commands
		

        public Command SaveImgToGalleryCommand
        {
            get
            {
                if (_saveImgToGalleryCommand == null)
                {
                    _saveImgToGalleryCommand = new Command(async postSelected =>
                    {
                        Uri postImage = new Uri(CurrentPost.PostDetail.Url, UriKind.Absolute);

                        bool isOnGallery = await _mediaService.SaveToGallery(postImage, CurrentPost.PostDetail.Url);

                        MessagingCenter.Send(this, AppConfig.ImageSaved);

                    });
                }
                return _saveImgToGalleryCommand;
            }
        }

        #endregion

        #region Private methods

        #endregion
    }
}
