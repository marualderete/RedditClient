using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using MyRedditApp.Models;
using MyRedditApp.Services.Interfaces;
using Newtonsoft.Json.Linq;

namespace MyRedditApp.Services
{
    public class PostService : DataService<PostStoreModel>, IPostService
    {
        #region Constructor

        public PostService()
        {
        }

        #endregion

        #region IPostService Implementation

        /// <summary>
        /// Dismisses all.
        /// </summary>
        /// <returns>The all.</returns>
        /// <param name="postList">Post list.</param>
        public async Task<bool> DismissAll(List<Post> postList)
        {
            if (!postList.Any())
                return false;

            foreach (Post eachPost in postList)
            {
                await DismissPost(eachPost.PostDetail.FullName);
            }

            return true;
        }

        /// <summary>
        /// Dismisses the post by its postId
        /// </summary>
        /// <returns>The post.</returns>
        /// <param name="postId">Post identifier.</param>
        public async Task<bool> DismissPost(string postId)
        {
            if (string.IsNullOrEmpty(postId))
                throw new ArgumentNullException("postId", "You must specify the fullname to dismiss a post");

            var url = AppConfig.API_URL + string.Format(AppConfig.Hide, postId);

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri($"{url}");
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", GetAuthService.GetToken());
                client.DefaultRequestHeaders.Add("User-Agent", "MyRedditApp/0.1 by Me");

                try
                {
                    var response = await client.GetAsync(new Uri(url));

                    if (response.IsSuccessStatusCode) //IRootResourceObject
                    {
                        var jsonStr = await response.Content.ReadAsStringAsync();

						return true;

                    }

                }
                catch (Exception e)
                {
                    throw new ArgumentNullException("Error", "Sorry, we are having some problems with the service");
                }

                return false;
            }
        }
		
        #endregion

    }
}
