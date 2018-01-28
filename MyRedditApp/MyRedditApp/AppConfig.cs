using System;
namespace MyRedditApp
{
    public static class AppConfig
    {
        //BASE API URL
        public static readonly string RedditBaseURL = "";

		//TOKEN
        //TODO: this should be separated in params 
        public static readonly string GetTokenURL = "https://www.reddit.com/api/v1/authorize?client_id=ClD-CsiBwgguyA&response_type=token&state=success202&redirect_uri=http://m.MyRedditApp.ferrison.com&scope=read,identity,report";


        public static readonly string API_URL = "https://oauth.reddit.com";

        public static readonly string TopPost = "https://oauth.reddit.com/top?limit=10";
        public static readonly string After = "&after={0}";
        public static readonly string Hide = "/hide?id={0}";

            

    }
}
