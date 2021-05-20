using BookeryApi.Services;
using WPF.State.Navigators;
using WPF.ViewModels.Factories;

namespace WPF.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public MainViewModel(IViewModelFactory viewModelFactory)
        {
            Navigator = new Navigator(viewModelFactory);
        }

        public INavigator Navigator { get; set; }
    }
}