using System;
using System.Windows;
using System.Windows.Input;
using Domain.Models;

namespace WPF.Commands
{
    internal class CopyShareIdCommand : ICommand
    {
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (parameter is Share share)
            {
                Clipboard.SetText(share.Id.ToString());
                MessageBox.Show("Copied to clipboard.");
            }
        }

        public event EventHandler CanExecuteChanged;
    }
}