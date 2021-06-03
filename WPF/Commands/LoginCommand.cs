using System;
using System.Threading.Tasks;
using BookeryApi.Exceptions;
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

            _loginViewModel.PropertyChanged += (sender, e) =>
            {
                if (e.PropertyName == nameof(LoginViewModel.CanLogin))
                {
                    OnCanExecuteChanged();
                }
            };
        }

        public override bool CanExecute(object parameter)
        {
            return _loginViewModel.CanLogin && base.CanExecute(parameter);
        }

        public override async Task ExecuteAsync(object parameter)
        {
            try
            {
                await _authenticator.Login(_loginViewModel.Email, _loginViewModel.Password);

                _renavigator.Renavigate();
            }
            catch (InvalidCredentialException e)
            {
                _loginViewModel.MessageViewModel.Message = e.Message;
            }
            catch (Exception)
            {
                _loginViewModel.MessageViewModel.Message = "Remote service is not available.";
            }
        }
    }
}