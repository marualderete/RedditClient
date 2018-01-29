using System;
using System.Net.Http;
using System.Threading.Tasks;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using MyRedditApp.Services.Interfaces;

using System.Net;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.IO;

namespace MyRedditApp.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        #region private attributes

        string _accessToken = "L-FYKnOXDG8Tw1xF15NshvMe8LA";

        #endregion

		#region constructor
        public AuthenticationService()
        {
        }
        #endregion

        #region private methods
   //     Task<bool> VerifyCurrentToken()
   //     {
			//var basicApiURL = AppConfig.API_URL; // just the basic url in order to Know if we need to make a new acess_token
            
			////Verify if current token is still valid:
			//using (var client = new HttpClient())
			//{
			//	client.BaseAddress = new Uri($"{basicApiURL}");
			//	client.DefaultRequestHeaders.Clear();
   //             client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", _accessToken);
			//	client.DefaultRequestHeaders.Add("User-Agent", "MyRedditApp/0.1 by Me");

   //             object result;

   //             try{
   //                 var verifyTask = Task.Factory.StartNew(() =>{
                        
			//			var response = client.GetAsync(new Uri(basicApiURL));
   //                     result = response.Result;
   //                 });
					
   //                 verifyTask.Wait();
   //                 var currentStatus = verifyTask.Status;

   //                 return Task.FromResult(true);
                    
   //             }catch(Exception e)
   //             {
   //                 throw new Exception();
   //             }
				
			//}
        //}


        #endregion

        #region IAuthentication implementation
        public async Task<bool> GetRequestToken()
        {

            var requestTokenURL = "https://www.reddit.com/api/v1/access_token";

            // POST: request a new access_token!!
            using (var client = new HttpClient())
            {
                try
                {
    //"https://www.reddit.com/api/v1/authorize?client_id=ClD-CsiBwgguyA&response_type=token&state=success202&redirect_uri=http://m.MyRedditApp.ferrison.com&scope=read,identity,report";
                    var values = new Dictionary<string, string>
                    {
                        { "grant_type", "refresh_token" },
                        { "client_id", "ClD-CsiBwgguyA" },
                        { "Content-Type", "MyRedditApp/x-www-form-urlencoded"},
                        { "refresh_token", _accessToken},
                        { "scope", "read,identity,report"},
                        { "state", "successss"},
                        { "duration", "temporary"},
                        { "redirect_uri", "MyRedditApp/x-www-form-urlencoded"}
                    };
					client.DefaultRequestHeaders.Clear();
					client.BaseAddress = new Uri($"{requestTokenURL}");
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic");
                    client.DefaultRequestHeaders.Add("User-Agent", "MyRedditApp/0.1 by Me");
                    //Basic Base64(client_Id:client_secret)
                    var content = new FormUrlEncodedContent(values);
                    var response = await client.PostAsync(new Uri(requestTokenURL), content);


					//var responseString = await response.Content.ReadAsStringAsync();

                    if (response.IsSuccessStatusCode)
                    {
                        string responseString = await response.Content.ReadAsStringAsync();
    					
    					//var newToken = JObject.Parse(content)["access_token"].ToString();
    					//_accessToken = newToken;
                        
                    }
                    //response.EnsureSuccessStatusCode();


                    return true;

                }
                catch (Exception e)
                {

                }

                return false;
            }
        }

        public string GetToken()
        {
            //Task.Run(async ()=>
            //{
            //    await GetRequestToken();
            //});

            return _accessToken;
        }

        #endregion

        #region Public Methods

        #endregion
    }
}
