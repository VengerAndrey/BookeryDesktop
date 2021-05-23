using System.Windows.Input;
using WPF.Commands;
using WPF.State.Authentication;
using WPF.State.Navigation;

namespace WPF.ViewModels
{
    internal class LoginViewModel : BaseViewModel
    {
        private string _email = "email@gmail.com";

        private string _password = "123";

        public LoginViewModel(IAuthenticator authenticator, IRenavigator loginRenavigator)
        {
            MessageViewModel = new MessageViewModel();

            LoginCommand = new LoginCommand(this, authenticator, loginRenavigator);
        }

        public MessageViewModel MessageViewModel { get; }

        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                OnPropertyChanged(nameof(Email));
                OnPropertyChanged(nameof(CanLogin));
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
                OnPropertyChanged(nameof(CanLogin));
            }
        }

        public ICommand LoginCommand { get; set; }

        public bool CanLogin => !string.IsNullOrEmpty(Email) && !string.IsNullOrEmpty(Password);
    }
}