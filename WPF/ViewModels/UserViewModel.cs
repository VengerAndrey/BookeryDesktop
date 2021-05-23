using System.Windows.Input;
using BookeryApi.Services.User;
using Domain.Models;
using WPF.Commands;
using WPF.State.Authentication;
using WPF.State.Navigation;

namespace WPF.ViewModels
{
    internal class UserViewModel : BaseViewModel
    {
        private User _user;

        public UserViewModel(IAuthenticator authenticator, IRenavigator loginRenavigator, IUserService userService)
        {
            MessageViewModel = new MessageViewModel();

            LoadUserCommand = new LoadUserCommand(this, userService);
            LoadUserCommand.Execute(null);

            LogoutCommand = new LogoutCommand(authenticator, loginRenavigator);
        }

        public User User
        {
            get => _user;
            set
            {
                _user = value;
                OnPropertyChanged(nameof(User));
            }
        }

        public MessageViewModel MessageViewModel { get; }

        public ICommand LoadUserCommand { get; }
        public ICommand LogoutCommand { get; }
    }
}