using System;

using MyRedditApp.Models;
using MyRedditApp.Services.Interfaces;

namespace MyRedditApp.Services
{
    public class PostService : DataService<PostStoreModel>, IPostService
    {
        #region Constructor

        public PostService()
        {
        }

        #endregion
    }
}
