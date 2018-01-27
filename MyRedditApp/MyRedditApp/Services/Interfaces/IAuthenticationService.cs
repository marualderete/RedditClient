using System;
using System.Threading.Tasks;

namespace MyRedditApp.Services
{
    public interface IAuthenticationService
    {
        Task<bool> Login(string user, string password);

        String GetToken();

    }
}
