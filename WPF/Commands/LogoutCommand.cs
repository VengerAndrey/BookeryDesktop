using System;
using System.Windows.Input;
using WPF.State.Authentication;
using WPF.State.Navigation;

namespace WPF.Commands
{
    internal class LogoutCommand : ICommand
    {
        private readonly IAuthenticator _authenticator;
        private readonly IRenavigator _renavigator;

        public LogoutCommand(IAuthenticator authenticator, IRenavigator renavigator)
        {
            _authenticator = authenticator;
            _renavigator = renavigator;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _authenticator.Logout();
            _renavigator.Renavigate();
        }

        public event EventHandler CanExecuteChanged;
    }
}