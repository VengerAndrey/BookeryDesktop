using System;
using System.Windows.Input;
using WPF.ViewModels;

namespace WPF.Commands
{
    internal class OpenDataInputCommand : ICommand
    {
        private readonly DataInputViewModel _dataInputViewModel;

        public OpenDataInputCommand(DataInputViewModel dataInputViewModel)
        {
            _dataInputViewModel = dataInputViewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (parameter is DataInputType dataInputType)
            {
                _dataInputViewModel.Show(dataInputType);
            }
        }

        public event EventHandler CanExecuteChanged;
    }
}