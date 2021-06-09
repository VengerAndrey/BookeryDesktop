using System;
using System.Windows.Input;
using WPF.ViewModels;

namespace WPF.Commands
{
    internal class AccessShareByIdCommand : ICommand
    {
        private readonly SharesViewModel _sharesViewModel;

        public AccessShareByIdCommand(SharesViewModel sharesViewModel)
        {
            _sharesViewModel = sharesViewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _sharesViewModel.DataInputViewModel.Show(DataInputType.ShareId);
        }

        public event EventHandler CanExecuteChanged;
    }
}