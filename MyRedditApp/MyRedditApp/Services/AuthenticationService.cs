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

        string _accessToken = "A7kqPZTMTsWZx8QZB7Zm_7kUAos";

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
            //TODO: NEED TO PUT POST DATA FOR REFRES THE TOKEN!
            var requestTokenURL = AppConfig.GetTokenURL;

            // POST: request a new access_token!!
            using (var client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = new Uri($"{requestTokenURL}");
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Add("User-Agent", "MyRedditApp/0.1 by Me");

                    var response = await client.GetAsync(new Uri(requestTokenURL));

                    if (response.IsSuccessStatusCode)
                    {
    					string content = await response.Content.ReadAsStringAsync();
    					
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
            return _accessToken;
        }

        #endregion

        #region Public Methods

        #endregion
    }
}
