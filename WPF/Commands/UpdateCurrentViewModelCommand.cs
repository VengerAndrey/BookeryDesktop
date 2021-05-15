using System;
using System.Windows.Input;
using BookeryApi.Services;
using WPF.State.Navigators;
using WPF.ViewModels;

namespace WPF.Commands
{
    internal class UpdateCurrentViewModelCommand : ICommand
    {
        private readonly INavigator _navigator;
        private readonly IStorageService _storageService;

        public UpdateCurrentViewModelCommand(INavigator navigator, IStorageService storageService)
        {
            _navigator = navigator;
            _storageService = storageService;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (parameter is ViewType viewType)
                switch (viewType)
                {
                    case ViewType.Files:
                        _navigator.CurrentViewModel = new ItemsViewModel(_storageService);
                        break;
                    case ViewType.Mock:
                        _navigator.CurrentViewModel = new MockViewModel();
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
        }
    }
}