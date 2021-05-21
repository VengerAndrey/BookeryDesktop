using System;
using System.Threading.Tasks;

namespace WPF.State.Authentication
{
    public interface IAuthenticator
    {
        bool IsLoggedIn { get; }

        event Action StateChanged;

        Task Login(string email, string password);
        void Logout();
    }
}