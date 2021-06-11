using System;
using System.Threading.Tasks;
using Domain.Models.DTOs.Responses;

namespace WPF.State.Authentication
{
    public interface IAuthenticator
    {
        bool IsLoggedIn { get; }

        event Action StateChanged;

        Task Login(string email, string password);
        Task<SignUpResult> SignUp(string email, string username, string password);

        void LogOut();
    }
}