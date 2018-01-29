using System;

using Xamarin.Forms;

using Newtonsoft.Json;

using MyRedditApp.Helpers;

namespace MyRedditApp.Models
{
    public class Post : ObservableObject
    {
        [JsonProperty("kind")]
        public String Kind { get; set; }

        [JsonProperty("data")]
        public PostDetail PostDetail { get; set; }


        #region public getters

		/// <summary>
        /// Gets the thumbnail image.
        /// </summary>
        /// <returns>The thumbnail image.</returns>
        public ImageSource GetThumbnailImage()
        {
            if (PostDetail.IsSelf || string.IsNullOrEmpty(PostDetail.Thumbnail))
            {
                return ImageSource.FromFile("no_image.png");
            }
            else
            {
                return ImageSource.FromUri(new Uri($"{PostDetail.Thumbnail}"));
            }

        }

        /// <summary>
        /// Gets the post image.
        /// </summary>
        /// <returns>The post image.</returns>
        public ImageSource GetPostImage()
        {
            if (!AppUtils.IsUrlImage(PostDetail.Url))
            {
                return ImageSource.FromFile("no_image.png");
            }
            else
            {
                return ImageSource.FromUri(new Uri($"{PostDetail.Url}"));
            }
        }

        /// <summary>
        /// Updates the dates ago string.
        /// </summary>
        public void UpdateDatesAgoString()
        {
            var postDate = AppUtils.UnixTimeStampToDateTime(PostDetail.CreatedDateUTC);

            var days = Convert.ToInt32((DateTime.Now - postDate).TotalDays);
            if (days == 1 || days == 0)
            {
                PostDetail.CreatedDateStr = "Today";
            }
            else 
            {
                PostDetail.CreatedDateStr = days + " days ago";   
            }
        }

        #endregion


    }
}
