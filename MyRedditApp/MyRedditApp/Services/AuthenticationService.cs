using System;
using System.Net.Http;
using System.Threading.Tasks;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using MyRedditApp.Services.Interfaces;

using System.Net;
using System.Collections.Generic;
using System.Net.Http.Headers;

namespace MyRedditApp.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        #region private attributes

        string _accessToken = "7psO-L_cFGlyr1zh-p_difAIxhg";

        #endregion

        public AuthenticationService()
        {
        }
        #region constructor


        #endregion

        #region private methods

        async Task<string> GetRequestToken()
        {
            //TODO: NEED TO PUT POST DATA FOR REFRES THE TOKEN!
            var url = string.Format(AppConfig.RefreshTokenURL, GetToken());

            using (var client = new HttpClient())
            {
                var json = await client.GetStringAsync(url);

                var result = await Task.Run(() => JsonConvert.DeserializeObject(json));
                var isOK = (bool)JObject.Parse(result.ToString())["success"];

                if (!isOK)
                {
                    return string.Empty;
                }

                return JObject.Parse(result.ToString())["request_token"].ToString();
            }
        }
        #endregion

        #region IAuthentication implementation

        /// <summary>
        /// Login the specified user and password.
        /// </summary>
        /// <returns>The login.</returns>
        /// <param name="user">User.</param>
        /// <param name="password">Password.</param>
        public async Task<bool> Login(string user, string password)
        {
            if (string.IsNullOrEmpty(user) || string.IsNullOrEmpty(password))
            {
                throw new ArgumentNullException("invalid user or password");
            }

            var url = string.Format(AppConfig.RedditBaseURL + "login");

            using (var client = new HttpClient())
            {
                HttpStatusCode code;
                string responseBody = string.Empty;
                string error = string.Empty;

                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var formContent = new FormUrlEncodedContent(new[] {
                    new KeyValuePair<string, string>("grant_type", "password"),
                    new KeyValuePair<string, string>("username", user),
                    new KeyValuePair<string, string>("password", password)
                });

                FormUrlEncodedContent postBody = formContent;

                Task taskDownload = client.PostAsync(url, postBody)
                    .ContinueWith(task =>
                    {

                        if (task.Status == TaskStatus.RanToCompletion)
                        {
                            var response = task.Result;
                            if (response.IsSuccessStatusCode)
                            {
                                code = response.StatusCode;
                                responseBody = response.Content.ReadAsStringAsync().Result;
                                //UserCredentialResponseModel credential = JsonConvert.DeserializeObject<UserCredentialResponseModel>(responseBody);
                                //_userCredentialResponseDatabase.AddItemAsync(credential);
                            }
                            else
                            {
                                code = response.StatusCode;
                                responseBody = response.Content.ReadAsStringAsync().Result;
                                //ErrorModel errormodel = JsonConvert.DeserializeObject<ErrorModel>(responseBody);

                                //error = errormodel.Error_Description;
                            }
                        }
                        else
                        {
                            error = "El servicio no se encuentra disponible. Intente mas tarde";
                        }
                    });

                taskDownload.Wait();

                if (!string.IsNullOrEmpty(error))
                    throw new ArgumentNullException(error);

                return string.IsNullOrEmpty(error);
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
