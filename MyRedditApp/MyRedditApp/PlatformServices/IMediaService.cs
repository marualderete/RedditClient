using System;
using System.Threading.Tasks;

namespace MyRedditApp.PlatformServices
{
    public interface IMediaService
    {
        Task<bool> SaveToGallery(Uri uri, string url);
    }
}
