using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyRedditApp.Models;

namespace MyRedditApp.Services.Interfaces
{
    public interface IPostService : IDataService<PostStoreModel>
    {
        Task<bool> DismissPost (string postId);
        Task<bool> DismissAll(List<Post> postList);

    }
}
