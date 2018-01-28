using System;
using MyRedditApp.Helpers;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace MyRedditApp.Models
{
    public class Post : ObservableObject
    {
        [JsonProperty("kind")]
        public String Kind { get; set; }

        [JsonProperty("data")]
        public PostDetail PostDetail { get; set; }

        [JsonIgnore]
        public String Prueba { get; set; }

        public ImageSource GetAuthorPhoto()
        {
            if (string.IsNullOrEmpty(PostDetail.ThumbnailURL))
            {
                return ImageSource.FromResource("userphoto.png");
            }
            else
            {
                return ImageSource.FromUri(new Uri($"{PostDetail.ThumbnailURL}"));
            }

        }
    }
}
