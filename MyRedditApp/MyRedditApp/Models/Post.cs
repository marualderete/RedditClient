using System;
using MyRedditApp.Helpers;
using Newtonsoft.Json;

namespace MyRedditApp.Models
{
    public class Post : ObservableObject
    {
        [JsonProperty("kind")]
        public String Kind { get; set; }

        [JsonProperty("data")]
        public PostDetail PostDetail { get; set; }

    }
}
