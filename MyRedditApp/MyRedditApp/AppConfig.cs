using System;
namespace MyRedditApp
{
    public static class AppConfig
    {
        //BASE API URL
        public static readonly string RedditBaseURL = "";

		//TOKEN
        //TODO: this should be separated in params 
        public static readonly string GetTokenURL = "https://www.reddit.com/api/v1/authorize?client_id=ClD-CsiBwgguyA&response_type=token&state=success202&redirect_uri=http://localhost&scope=read,identity,report";
        public static readonly string RefreshTokenURL = "https://www.reddit.com/api/v1/revoke_token";
        public static readonly string RefreshTokenPOST_Data = "token=%s&token_type_hint=refresh_token"; //%s is a param to be used in String.Format(...)

        public static readonly string API_URL = "https://oauth.reddit.com";
        public static readonly string TopPost = "https://oauth.reddit.com/top?limit=5";
        public static readonly string After = "?after=%s";

            

    }
}
