using System;
using System.Threading.Tasks;
using BookeryApi.Exceptions;
using WPF.State.Authentication;
using WPF.State.Navigation;
using WPF.ViewModels;

namespace WPF.Commands
{
    internal class SignInCommand : AsyncCommand
    {
        private readonly IAuthenticator _authenticator;
        private readonly IRenavigator _renavigator;
        private readonly SignInViewModel _signInViewModel;

        public SignInCommand(SignInViewModel signInViewModel, IAuthenticator authenticator, IRenavigator renavigator)
        {
            _signInViewModel = signInViewModel;
            _authenticator = authenticator;
            _renavigator = renavigator;

            _signInViewModel.PropertyChanged += (sender, e) =>
            {
                if (e.PropertyName == nameof(SignInViewModel.CanLogin))
                {
                    OnCanExecuteChanged();
                }
            };
        }

        public override bool CanExecute(object parameter)
        {
            return _signInViewModel.CanLogin && base.CanExecute(parameter);
        }

        public override async Task ExecuteAsync(object parameter)
        {
            try
            {
                await _authenticator.Login(_signInViewModel.Email, _signInViewModel.Password);

                _renavigator.Renavigate();
            }
            catch (InvalidCredentialException e)
            {
                _signInViewModel.MessageViewModel.Message = e.Message;
            }
            catch (Exception)
            {
                _signInViewModel.MessageViewModel.Message = "Remote service is not available.";
            }
        }
    }
}