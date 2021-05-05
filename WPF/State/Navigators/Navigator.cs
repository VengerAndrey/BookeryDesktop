using System.Windows.Input;
using WPF.Commands;
using WPF.Models;
using WPF.ViewModels;

namespace WPF.State.Navigators
{
    public class Navigator : ObservableObject, INavigator
    {
        private BaseViewModel _currentViewModel;

        public BaseViewModel CurrentViewModel
        {
            get => _currentViewModel;
            set
            {
                _currentViewModel = value;
                OnPropertyChanged(nameof(CurrentViewModel));
            }
        }
        public ICommand UpdateCurrentViewModelCommand => new UpdateCurrentViewModelCommand(this);
    }
}
