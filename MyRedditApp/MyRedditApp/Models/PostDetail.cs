using System;

using MyRedditApp.Helpers;
using Newtonsoft.Json;

using SQLite;
using Xamarin.Forms;

namespace MyRedditApp.Models
{
    public class PostDetail : ObservableObject
    {
        [PrimaryKey]
        [JsonProperty("id")]
        public String Id { get; set; }

        [JsonProperty("subreddit_id")]
        public String SubredditId { get; set; }

        [JsonProperty("title")]
        public String Title { get; set; }

        [JsonProperty("author")]
        public String Author { get; set; }

        [JsonProperty("created")]
        public int CreatedDate { get; set; }

        [JsonProperty("created_utc")]
        public int CreatedDateUTC { get; set; }

        [JsonIgnore]
        public String CreatedDateStr { get; set; }

        [JsonProperty("thumbnail")]
        public String Thumbnail { get; set; }

        [JsonProperty("num_comments")]
        public int CommentCount { get; set; }

        [JsonProperty("clicked")]
        public bool IsClicked { get; set; }

        [JsonProperty("hidden")]
        public bool IsHidden { get; set; }

        [JsonProperty("name")]
        public String FullName { get; set; }

        [JsonProperty("url")]
        public String Url { get; set; }

        [JsonProperty("is_video")]
        public bool IsVideo { get; set; }
    }
}
