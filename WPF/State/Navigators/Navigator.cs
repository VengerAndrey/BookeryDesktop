using System.Windows.Input;
using BookeryApi.Services;
using WPF.Commands;
using WPF.Models;
using WPF.ViewModels;
using WPF.ViewModels.Factories;

namespace WPF.State.Navigators
{
    public class Navigator : ObservableObject, INavigator
    {
        private readonly IViewModelFactory _viewModelFactory;
        private BaseViewModel _currentViewModel;

        public Navigator(IViewModelFactory viewModelFactory)
        {
            _viewModelFactory = viewModelFactory;
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

        public ICommand UpdateCurrentViewModelCommand => new UpdateCurrentViewModelCommand(this, _viewModelFactory);
    }
}