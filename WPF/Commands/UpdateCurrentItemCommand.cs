using System;
using System.Windows.Input;
using Domain.Models;
using WPF.ViewModels;

namespace WPF.Commands
{
    internal class UpdateCurrentItemCommand : ICommand
    {
        private readonly HomeViewModel _homeViewModel;

        public UpdateCurrentItemCommand(HomeViewModel homeViewModel)
        {
            _homeViewModel = homeViewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (parameter is Item item)
            {
                _homeViewModel.CurrentItem = item;
            }
        }

        public event EventHandler CanExecuteChanged;
    }
}