using System;
using System.Resources;
using System.Text.RegularExpressions;
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

        public ImageSource GetThumbnailImage()
        {
            if (string.IsNullOrEmpty(PostDetail.Thumbnail))
            {
                return ImageSource.FromResource("no_image.png");
            }
            else
            {
                return ImageSource.FromUri(new Uri($"{PostDetail.Thumbnail}"));
            }

        }

        public ImageSource GetPostImage()
        {
            Regex regexPattern = new Regex(@"(?:([^:\?#]+):)?(?:\\([^\?#]*))?([^?#]*\.(?:jpg|JPEG|png|gif|gifv))(?:\?([^#]*))?(?:#(.*))?", RegexOptions.IgnoreCase);

            bool isImage = regexPattern.Match(PostDetail.Url).Success;

            if (!isImage)
            {
                return ImageSource.FromFile("no_image.png");
            }
            else
            {
                return ImageSource.FromUri(new Uri($"{PostDetail.Url}"));
            }
        }
    }
}
