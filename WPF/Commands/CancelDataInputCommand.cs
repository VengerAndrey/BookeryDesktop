using System;
using System.Windows.Input;
using WPF.ViewModels;

namespace WPF.Commands
{
    internal class CancelDataInputCommand : ICommand
    {
        private readonly DataInputViewModel _dataInputViewModel;

        public CancelDataInputCommand(DataInputViewModel dataInputViewModel)
        {
            _dataInputViewModel = dataInputViewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _dataInputViewModel.Name = "";
            _dataInputViewModel.Value = "";
        }

        public event EventHandler CanExecuteChanged;
    }
}