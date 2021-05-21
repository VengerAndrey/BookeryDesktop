using System.Threading.Tasks;
using WPF.State.Authentication;
using WPF.State.Navigation;
using WPF.ViewModels;

namespace WPF.Commands
{
    internal class LoginCommand : AsyncCommand
    {
        private readonly IAuthenticator _authenticator;
        private readonly LoginViewModel _loginViewModel;
        private readonly IRenavigator _renavigator;

        public LoginCommand(LoginViewModel loginViewModel, IAuthenticator authenticator, IRenavigator renavigator)
        {
            _loginViewModel = loginViewModel;
            _authenticator = authenticator;
            _renavigator = renavigator;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            await _authenticator.Login(_loginViewModel.Email, _loginViewModel.Password);

            _renavigator.Renavigate();
        }
    }
}