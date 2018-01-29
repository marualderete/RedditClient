using System;
using System.Threading.Tasks;

using Xamarin.Forms;

using MyRedditApp.Droid.PlatformServices;
using MyRedditApp.PlatformServices;
using Android.Content;
using System.IO;
using Android.Widget;
using Android.Provider;
using Android.Graphics;
using Java.Net;
using System.Net;

[assembly: Dependency(typeof(MediaService))]
namespace MyRedditApp.Droid.PlatformServices
{
    public class MediaService : IMediaService
    {
        private readonly Context context = Android.App.Application.Context;

        #region IMediaService implementations

        /// <summary>
        /// Saves to gallery.
        /// </summary>
        /// <returns>The to gallery.</returns>
        /// <param name="uri">URI.</param>
        /// <param name="url">URL.</param>
		public Task<bool> SaveToGallery(Uri uri, string url)
        {
            try
            {
                // Get the MainActivity instance
                MainActivity activity = Forms.Context as MainActivity;
                var webClient = new WebClient();

                webClient.DownloadDataCompleted += (s, e) =>
                {
                    var bitmap = GetBitmapFromURL(url);
                    MediaStore.Images.Media.InsertImage(activity.ContentResolver, bitmap, "Testin1", "testing1");
                };

                //First we need to download the image from the url, in order to transform it.
                webClient.DownloadDataAsync(uri);

            }
            catch (Exception ex)
            {
                return Task.FromResult(false);
            }

            return Task.FromResult(true);
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Gets the bitmap from URL.
        /// </summary>
        /// <returns>The bitmap from URL.</returns>
        /// <param name="url">URL.</param>
        public Bitmap GetBitmapFromURL(String url)
        {
            try
            {
                WebRequest request = System.Net.WebRequest.Create(url);
                WebResponse response = request.GetResponse();
                Stream responseStream = response.GetResponseStream();

                Bitmap bitmapResult = BitmapFactory.DecodeStream(responseStream);

                return bitmapResult;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        #endregion
    }
}
