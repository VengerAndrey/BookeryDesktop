using System;
using System.Threading.Tasks;
using Domain.Models.DTOs.Responses;
using WPF.State.Authentication;
using WPF.State.Navigation;
using WPF.ViewModels;

namespace WPF.Commands
{
    internal class SignUpCommand : AsyncCommand
    {
        private readonly IAuthenticator _authenticator;
        private readonly IRenavigator _signInRenavigator;
        private readonly SignUpViewModel _signUpViewModel;

        public SignUpCommand(SignUpViewModel signUpViewModel, IAuthenticator authenticator,
            IRenavigator signInRenavigator)
        {
            _signUpViewModel = signUpViewModel;
            _authenticator = authenticator;
            _signInRenavigator = signInRenavigator;

            _signUpViewModel.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == nameof(SignUpViewModel.CanSignUp))
                {
                    OnCanExecuteChanged();
                }
            };
        }

        public override bool CanExecute(object parameter)
        {
            return _signUpViewModel.CanSignUp && base.CanExecute(parameter);
        }

        public override async Task ExecuteAsync(object parameter)
        {
            try
            {
                var signUpResult = await _authenticator.SignUp(_signUpViewModel.Email, _signUpViewModel.Username,
                    _signUpViewModel.Password);

                switch (signUpResult)
                {
                    case SignUpResult.Success:
                        _signInRenavigator.Renavigate();
                        break;
                    case SignUpResult.EmailAlreadyExists:
                        _signUpViewModel.MessageViewModel.Message = "Email already exists.";
                        break;
                    case SignUpResult.UsernameAlreadyExists:
                        _signUpViewModel.MessageViewModel.Message = "Username already exists.";
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            catch (Exception)
            {
                _signUpViewModel.MessageViewModel.Message = "Remote service is not available.";
            }
        }
    }
}