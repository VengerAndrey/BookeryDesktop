﻿using System;
using System.Windows.Input;
using WPF.State.Authentication;
using WPF.State.Navigation;

namespace WPF.Commands
{
    internal class LogOutCommand : ICommand
    {
        private readonly IAuthenticator _authenticator;
        private readonly IRenavigator _renavigator;

        public LogOutCommand(IAuthenticator authenticator, IRenavigator renavigator)
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
            _authenticator.LogOut();
            _renavigator.Renavigate();
        }

        public event EventHandler CanExecuteChanged;
    }
}