using System;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MyRedditApp.PlatformServices
{
    public interface IMediaService
    {
        Task<bool> SaveToGallery(Uri uri, string url);
    }
}
