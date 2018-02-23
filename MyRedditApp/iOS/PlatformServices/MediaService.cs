using System;
using System.Threading.Tasks;

using Xamarin.Forms;

using Foundation;

using UIKit;

using MyRedditApp.iOS.PlatformServices;
using MyRedditApp.PlatformServices;

[assembly: Dependency(typeof(MediaService))]
namespace MyRedditApp.iOS.PlatformServices
{
    public class MediaService : IMediaService
    {
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
                UIImage photoToSave = UIImageFromUrl(url);
				
				photoToSave.SaveToPhotosAlbum((image, error) => {
					Console.WriteLine("error:" + error);
				});
				
				photoToSave.Dispose();
                
            }
            catch(Exception ex)
            {
                return Task.FromResult(false);
            }

            return Task.FromResult(true);
        }
        #endregion

        #region Private methods

        /// <summary>
        /// UIIs the mage from URL.
        /// </summary>
        /// <returns>The mage from URL.</returns>
        /// <param name="uri">URI.</param>
        public static UIImage UIImageFromUrl(string uri)
        {
            using (var url = new NSUrl(uri))
            using (var data = NSData.FromUrl(url))
                return UIImage.LoadFromData(data);
        }
		#endregion
    }
}
