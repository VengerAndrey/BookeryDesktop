using System.Windows.Input;
using WPF.Commands;
using WPF.State.Authentication;
using WPF.State.Navigation;

namespace WPF.ViewModels
{
    internal class SignUpViewModel : BaseViewModel
    {
        private string _confirmPassword;
        private string _email;

        private string _password;

        private string _username;

        public SignUpViewModel(IAuthenticator authenticator, IRenavigator signInRenavigator)
        {
            MessageViewModel = new MessageViewModel();

            SignUpCommand = new SignUpCommand(this, authenticator, signInRenavigator);
            ViewSignInCommand = new RenavigateCommand(signInRenavigator);
        }

        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                OnPropertyChanged(nameof(Email));
                OnPropertyChanged(nameof(CanSignUp));
            }
        }

        public string Username
        {
            get => _username;
            set
            {
                _username = value;
                OnPropertyChanged(nameof(Username));
                OnPropertyChanged(nameof(CanSignUp));
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
                OnPropertyChanged(nameof(CanSignUp));
            }
        }

        public string ConfirmPassword
        {
            get => _confirmPassword;
            set
            {
                _confirmPassword = value;
                OnPropertyChanged(nameof(ConfirmPassword));
                OnPropertyChanged(nameof(CanSignUp));
            }
        }

        public bool CanSignUp => !string.IsNullOrEmpty(Email) &&
                                 !string.IsNullOrEmpty(Username) &&
                                 !string.IsNullOrEmpty(Password) &&
                                 !string.IsNullOrEmpty(ConfirmPassword) &&
                                 Password == ConfirmPassword;

        public ICommand SignUpCommand { get; }
        public ICommand ViewSignInCommand { get; }

        public MessageViewModel MessageViewModel { get; }
    }
}