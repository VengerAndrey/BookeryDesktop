using WPF.State.Navigators;

namespace WPF.ViewModels.Factories
{
    internal interface IViewModelFactory
    {
        BaseViewModel CreateViewModel(ViewType viewType);
    }
}