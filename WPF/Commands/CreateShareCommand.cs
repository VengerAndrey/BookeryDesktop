using System;
using System.Windows.Input;
using WPF.ViewModels;

namespace WPF.Commands
{
    internal class CreateShareCommand : ICommand
    {
        private readonly SharesViewModel _sharesViewModel;

        public CreateShareCommand(SharesViewModel sharesViewModel)
        {
            _sharesViewModel = sharesViewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _sharesViewModel.DataInputViewModel.Show(DataInputType.ShareName);
        }

        public event EventHandler CanExecuteChanged;
    }
}