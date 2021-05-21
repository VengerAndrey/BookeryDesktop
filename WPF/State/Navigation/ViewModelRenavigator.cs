using WPF.ViewModels;

namespace WPF.State.Navigation
{
    internal class ViewModelRenavigator<TViewModel> : IRenavigator where TViewModel : BaseViewModel
    {
        private readonly CreateViewModel<TViewModel> _createViewModel;
        private readonly INavigator _navigator;

        public ViewModelRenavigator(INavigator navigator, CreateViewModel<TViewModel> createViewModel)
        {
            _navigator = navigator;
            _createViewModel = createViewModel;
        }


        public void Renavigate()
        {
            _navigator.CurrentViewModel = _createViewModel();
        }
    }
}