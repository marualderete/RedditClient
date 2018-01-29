﻿using System;
using System.Collections.Generic;
using System.Linq;

using Newtonsoft.Json;

using MyRedditApp.Helpers;

namespace MyRedditApp.Models
{
    public class PostStoreModel : ObservableObject
    {
        [JsonProperty("after")]
        public String After { get; set; }

        List<Post> postChildren = new List<Post>();

        [JsonProperty(PropertyName = "children")]
        public Post[] PostChildren
        {
            get { return postChildren.ToArray(); }
            set { SetProperty(ref postChildren, value.ToList()); }
        }
    }
}
