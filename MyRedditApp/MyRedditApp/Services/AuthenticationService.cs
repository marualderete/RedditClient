using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;

using Newtonsoft.Json.Linq;

namespace MyRedditApp.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        #region private attributes

        string _accessToken = "5N3xtUVo9WvejrfBpIbWRqYWWbs";

        #endregion

		#region constructor
        public AuthenticationService()
        {
        }
        #endregion

        #region private methods

        #endregion

        #region IAuthentication implementation

        /// <summary>
        /// Gets the request token.
        /// </summary>
        /// <returns>The request token.</returns>
        public async Task<bool> GetRequestToken()
        {

            var requestTokenURL = "https://www.reddit.com/api/v1/access_token";

            // POST: request a new access_token!!
            using (var client = new HttpClient())
            {
                try
                {
                    byte[] authorizationData = System.Text.Encoding.UTF8.GetBytes(string.Format("{0}:''", "ClD-CsiBwgguyA"));

                    var uniqueId = Guid.NewGuid();

                    var values = new Dictionary<string, string>
                    {
                        { "grant_type", "https://oauth.reddit.com/grants/installed_client" },
                        { "client_id", "ClD-CsiBwgguyA" },
                        { "device_id", uniqueId.ToString() },
                    };

                    String username = "ClD-CsiBwgguyA";
                    String password = "";
                    String encoded = System.Convert.ToBase64String(System.Text.Encoding.GetEncoding("ISO-8859-1").GetBytes(username + ":" + password));
					
					client.BaseAddress = new Uri($"{requestTokenURL}");
                    client.DefaultRequestHeaders.Add("Authorization", "Basic " + encoded);
                    client.DefaultRequestHeaders.Add("User-Agent", "MyRedditApp/0.1 by Me");

                    var content = new FormUrlEncodedContent(values);
                    var response = await client.PostAsync(new Uri(requestTokenURL), content);


                    if (response.IsSuccessStatusCode)
                    {
                        string responseString = await response.Content.ReadAsStringAsync();
    					
                        var newToken = JObject.Parse(responseString)["access_token"].ToString();
    					_accessToken = newToken;
                        
                    }

                    return true;

                }
                catch (Exception e)
                {

					return false;
                }

            }
        }

        public string GetToken()
        {
            return _accessToken;
        }

        #endregion

        #region Public Methods

        #endregion
    }
}
