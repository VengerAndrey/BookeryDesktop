using System.Windows.Input;
using BookeryApi.Services;
using WPF.Commands;
using WPF.Models;
using WPF.ViewModels;

namespace WPF.State.Navigators
{
    public class Navigator : ObservableObject, INavigator
    {
        private readonly IStorageService _storageService;
        private BaseViewModel _currentViewModel;

        public Navigator(IStorageService storageService)
        {
            _storageService = storageService;
        }

        public BaseViewModel CurrentViewModel
        {
            get => _currentViewModel;
            set
            {
                _currentViewModel = value;
                OnPropertyChanged(nameof(CurrentViewModel));
            }
        }

        public ICommand UpdateCurrentViewModelCommand => new UpdateCurrentViewModelCommand(this, _storageService);
    }
}