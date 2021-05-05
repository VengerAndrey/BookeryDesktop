using System.Windows.Input;
using WPF.ViewModels;

namespace WPF.State.Navigators
{
    public interface INavigator
    {
        BaseViewModel CurrentViewModel { get; set; }
        ICommand UpdateCurrentViewModelCommand { get; }
    }

    public enum ViewType
    {
        Containers,
        Mock
    }
}
