﻿using System.Threading.Tasks;
using System.Windows.Input;

using Xamarin.Forms;

using MyRedditApp.Models;

namespace MyRedditApp.CustomViews
{
    public partial class PostCell : Frame
    {
        #region Constructor

        public PostCell()
        {
            InitializeComponent();

        }

        #endregion

        #region Properties

        public ImageSource PostThumbnail
        {
            get { return CircleImagePhoto.Source; }
            set { CircleImagePhoto.Source = value; }
        }

        public string Author
        {
            get { return AuthorName.Text; }
            set { AuthorName.Text = value; }
        }

        public string CommentsCount
        {
            get { return CommentsNro.Text; }
            set { CommentsNro.Text = value; }
        }

        public string CreatedDate
        {
            get { return CreatedAt.Text; }
            set { CreatedAt.Text = value; }
        }

        public string PostTitle
        {
            get { return PostTitleLabel.Text; }
            set { PostTitleLabel.Text = value; }
        }


        #endregion

        #region Bindable Properties

        /// <summary>
        /// The post data property.
        /// </summary>
        public static readonly BindableProperty PostDataProperty = BindableProperty.Create(
            "PostData",
            typeof(Post),
            typeof(PostCell),
            null,
            propertyChanged: (bindable, oldValue, newValue) => 
            {
                ((PostCell)bindable).LoadPostData((Post) newValue);
            }
        );

        /// <summary>
        /// Gets or sets the post data.
        /// </summary>
        /// <value>The post data.</value>
        public Post PostData
        {
            get{ return (Post) this.GetValue(PostDataProperty); }
            set{ this.SetValue(PostDataProperty, value); }
        }

        /// <summary>
        /// The dismiss command property.
        /// </summary>
        public static readonly BindableProperty OnSelectedCommandProperty = BindableProperty.Create(
            "OnSelectedCommand",
            typeof(ICommand),
            typeof(PostCell),
            new Command(() => { }),
            propertyChanged: (bindable, oldValue, newValue) =>
            {
                ((PostCell)bindable).OnSelectedCommandChanged();
            });

		/// <summary>
		/// Gets or sets the dismiss command.
		/// </summary>
		/// <value>The dismiss command.</value>
		public ICommand OnSelectedCommand
		{
			get { return (ICommand)GetValue(OnSelectedCommandProperty); }
			set { SetValue(OnSelectedCommandProperty, value); }
		}

		/// <summary>
		/// The dismiss command property.
		/// </summary>
		public static readonly BindableProperty DismissCommandProperty = BindableProperty.Create(
			"DismissCommand",
			typeof(ICommand),
			typeof(PostCell),
			new Command(() => { }),
			propertyChanged: (bindable, oldValue, newValue) =>
		{
			((PostCell)bindable).DismissCommandChanged();
		});
		
		/// <summary>
		/// Gets or sets the dismiss command.
		/// </summary>
		/// <value>The dismiss command.</value>
		public ICommand DismissCommand
		{
			get { return (ICommand)GetValue(DismissCommandProperty); }
			set { SetValue(DismissCommandProperty, value); }
		}

        #endregion

        #region Private methods

        /// <summary>
        /// Dismisses the command changed.
        /// </summary>
        void OnSelectedCommandChanged()
        {
            var tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped += async (s, e) =>
            {
                CellBody.Opacity = .5;
                await Task.Delay(100);
                CellBody.Opacity = 1;
                OnSelectedCommand.Execute(PostData);
            };

            CellBody.GestureRecognizers.Add(tapGestureRecognizer);
        }

        /// <summary>
        /// Dismisses the command changed.
        /// </summary>
        void DismissCommandChanged()
        {
            var tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped += (s, e) =>
            {
                this.TranslateTo(-400, this.Y, 500);
                DismissCommand.Execute(PostData.PostDetail.FullName);
            };

            DismissButton.GestureRecognizers.Add(tapGestureRecognizer);
        }

        /// <summary>
        /// Loads the post data.
        /// </summary>
        /// <param name="newPost">New post.</param>
        void LoadPostData(Post newPost)
        {
            if(newPost != null)
            {
                PostThumbnail = newPost.GetThumbnailImage();
    			Author = newPost.PostDetail.Author;
                CreatedDate = newPost.PostDetail.CreatedDateStr;
    			PostTitle = newPost.PostDetail.Title;
                CommentsCount = newPost.PostDetail.CommentCount.ToString()+ " Comments";
            }
        }
		#endregion
    }
}
