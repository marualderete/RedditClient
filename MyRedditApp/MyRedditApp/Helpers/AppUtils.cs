using System;
using System.Text.RegularExpressions;

namespace MyRedditApp.Helpers
{
    public static class AppUtils
    {
        /// <summary>
        /// Unixs the time stamp to date time.
        /// </summary>
        /// <returns>The time stamp to date time.</returns>
        /// <param name="unixTimeStamp">Unix time stamp.</param>
        public static DateTime UnixTimeStampToDateTime(double unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dtDateTime;
        }

        /// <summary>
        /// Ises the URL image.
        /// </summary>
        /// <returns><c>true</c>, if URL image was ised, <c>false</c> otherwise.</returns>
        /// <param name="url">URL.</param>
        public static bool IsUrlImage(string url)
        {
            Regex regexPattern = new Regex(@"(?:([^:\?#]+):)?(?:\\([^\?#]*))?([^?#]*\.(?:jpg|JPEG|png|gif|gifv))(?:\?([^#]*))?(?:#(.*))?", RegexOptions.IgnoreCase);
            return regexPattern.Match(url).Success;

        }
    }
}
