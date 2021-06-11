using System;
using System.Windows.Input;
using Domain.Models;
using WPF.ViewModels;

namespace WPF.Commands
{
    internal class UpdateContextMenuItemCommand : ICommand
    {
        private readonly ItemsViewModel _itemsViewModel;

        public UpdateContextMenuItemCommand(ItemsViewModel itemsViewModel)
        {
            _itemsViewModel = itemsViewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (parameter is Item item)
            {
                _itemsViewModel.ContextMenuItem = item;
            }
        }

        public event EventHandler CanExecuteChanged;
    }
}