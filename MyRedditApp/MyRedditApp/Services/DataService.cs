using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using MyRedditApp.Helpers;
using MyRedditApp.Models;
using MyRedditApp.Services.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MyRedditApp.Services
{
    public class DataService<T> : IDataService<T>
    {
        #region Private

        IAuthenticationService _authenticationService = ServiceLocator.Instance.Get<IAuthenticationService>();
        #endregion

        #region Constructor
        public DataService() { }
        #endregion

        #region IDataService implementation

        /// <summary>
        /// Gets the item async.
        /// </summary>
        /// <returns>The item async.</returns>
        /// <param name="concreteSearch">Concrete search.</param>
        /// <param name="tokenType">Token type.</param>
        /// <param name="accessToken">Access token.</param>
        public async Task<T> GetItemAsync(string concreteSearch, string after = null)
        {

            if (string.IsNullOrEmpty(concreteSearch))
                throw new ArgumentNullException("concreteSearch", "You must specify what you are looking for");

            var url = GetAPIUrl(concreteSearch);

			if(after != null)
			{
                url += String.Format(AppConfig.After, after);	
			}

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri($"{url}");
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", _authenticationService.GetToken());
                client.DefaultRequestHeaders.Add("User-Agent", "MyRedditApp/0.1 by Me");

                try
                {
                    var response = await client.GetAsync(new Uri(url));

                    if (response.IsSuccessStatusCode) //IRootResourceObject
                    {

                        var jsonStr = await response.Content.ReadAsStringAsync();


                        var jObjectParsed = JObject.Parse(jsonStr)["data"];

                        T result = jObjectParsed.ToObject<T>();

                        return result;

                    }

                }
                catch (Exception e)
                {
                    throw new ArgumentNullException("Error", "Sorry, we are having some problems with the service");
                }

                return default(T);
            }


        }

        /// <summary>
        /// Gets the item list async.
        /// </summary>
        /// <returns>The item list async.</returns>
        /// <param name="concreteSearch">Concrete search.</param>
        /// <param name="tokenType">Token type.</param>
        /// <param name="accessToken">Access token.</param>
        /// <param name="forceRefresh">If set to <c>true</c> force refresh.</param>
        public async Task<IEnumerable<T>> GetItemListAsync(string concreteSearch)
        {
            throw new NotImplementedException();
            
        }

        #endregion

        #region private methods

        /// <summary>
        /// Gets the APIU rl.
        /// </summary>
        /// <returns>The APIU rl.</returns>
        /// <param name="concreteSearch">Concrete search.</param>
        string GetAPIUrl(string concreteSearch)
        {
            return string.Format(AppConfig.RedditBaseURL + concreteSearch);
        }

        #endregion
    }
}
