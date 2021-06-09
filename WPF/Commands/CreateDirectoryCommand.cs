using System;
using System.Windows.Input;
using WPF.ViewModels;

namespace WPF.Commands
{
    internal class CreateDirectoryCommand : ICommand
    {
        private readonly ItemsViewModel _itemsViewModel;

        public CreateDirectoryCommand(ItemsViewModel itemsViewModel)
        {
            _itemsViewModel = itemsViewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _itemsViewModel.DataInputViewModel.Show(DataInputType.DirectoryName);
            _itemsViewModel.DataInputCommand.Execute(parameter);
        }

        public event EventHandler CanExecuteChanged;
    }
}