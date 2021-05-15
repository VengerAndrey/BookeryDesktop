using BookeryApi.Services;
using WPF.State.Navigators;

namespace WPF.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public MainViewModel(IStorageService storageService)
        {
            Navigator = new Navigator(storageService);
        }

        public INavigator Navigator { get; set; }
    }
}