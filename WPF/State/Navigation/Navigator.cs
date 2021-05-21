using System;
using System.Windows.Input;
using WPF.Commands;
using WPF.ViewModels;
using WPF.ViewModels.Factories;

namespace WPF.State.Navigation
{
    public class Navigator : INavigator
    {
        private readonly IViewModelFactory _viewModelFactory;
        private BaseViewModel _currentViewModel;

        public Navigator(IViewModelFactory viewModelFactory)
        {
            _viewModelFactory = viewModelFactory;
        }

        public ICommand UpdateCurrentViewModelCommand => new UpdateCurrentViewModelCommand(this, _viewModelFactory);

        public BaseViewModel CurrentViewModel
        {
            get => _currentViewModel;
            set
            {
                _currentViewModel = value;
                StateChanged?.Invoke();
            }
        }

        public event Action StateChanged;
    }
}