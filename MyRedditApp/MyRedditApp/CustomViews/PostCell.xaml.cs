using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using MyRedditApp.Models;
using Xamarin.Forms;

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
        public ImageSource UserPhoto
        {
            get { return CircleImagePhoto.Source; }
            set { CircleImagePhoto.Source = value; }
        }

        public string Author
        {
            get { return AuthorName.Text; }
            set { AuthorName.Text = value; }
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
                PostCellBody.Opacity = .5;
                await Task.Delay(100);
                PostCellBody.Opacity = 1;
                OnSelectedCommand.Execute(PostData);
            };

            PostDetailContainer.GestureRecognizers.Add(tapGestureRecognizer);
        }

        void DismissCommandChanged()
        {
            var tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped += async (s, e) =>
            {
                DismissContainer.Opacity = .5;
                await Task.Delay(100);
                DismissContainer.Opacity = 1;
                DismissCommand.Execute(PostData.PostDetail.FullName);
            };

            DismissContainer.GestureRecognizers.Add(tapGestureRecognizer);
        }

        void LoadPostData(Post newPost)
        {
            UserPhoto = newPost.GetAuthorPhoto();
            Author = newPost.PostDetail.Author;
            CreatedDate = " n days ago ";
            PostTitle = newPost.PostDetail.Title;
        }
		#endregion
    }
}
