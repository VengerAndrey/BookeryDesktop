using System.Windows.Input;
using WPF.Commands;
using WPF.State.Authentication;
using WPF.State.Navigation;
using WPF.ViewModels.Factories;

namespace WPF.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private readonly IAuthenticator _authenticator;
        private readonly INavigator _navigator;

        public MainViewModel(IViewModelFactory viewModelFactory, INavigator navigator, IAuthenticator authenticator)
        {
            _navigator = navigator;
            _authenticator = authenticator;

            _authenticator.StateChanged += () => { OnPropertyChanged(nameof(IsLoggedIn)); };

            _navigator.StateChanged += () => { OnPropertyChanged(nameof(CurrentViewModel)); };

            UpdateCurrentViewModelCommand = new UpdateCurrentViewModelCommand(navigator, viewModelFactory);
            UpdateCurrentViewModelCommand.Execute(ViewType.Login);
        }

        public bool IsLoggedIn => _authenticator.IsLoggedIn;
        public BaseViewModel CurrentViewModel => _navigator.CurrentViewModel;

        public ICommand UpdateCurrentViewModelCommand { get; }
    }
}