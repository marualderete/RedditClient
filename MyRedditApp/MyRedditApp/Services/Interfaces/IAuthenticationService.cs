using System;
using System.Threading.Tasks;

namespace MyRedditApp.Services
{
    public interface IAuthenticationService
    {
        Task<bool> GetRequestToken();

        String GetToken();
    }
}
