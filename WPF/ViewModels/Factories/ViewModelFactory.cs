using System;
using WPF.State.Navigation;

namespace WPF.ViewModels.Factories
{
    internal class ViewModelFactory : IViewModelFactory
    {
        private readonly CreateViewModel<HomeViewModel> _createHomeViewModel;
        private readonly CreateViewModel<LoginViewModel> _createLoginViewModel;
        private readonly CreateViewModel<UserViewModel> _createUserViewModel;

        public ViewModelFactory(CreateViewModel<LoginViewModel> createLoginViewModel,
            CreateViewModel<HomeViewModel> createHomeViewModel,
            CreateViewModel<UserViewModel> createUserViewModel)
        {
            _createLoginViewModel = createLoginViewModel;
            _createHomeViewModel = createHomeViewModel;
            _createUserViewModel = createUserViewModel;
        }

        public BaseViewModel CreateViewModel(ViewType viewType)
        {
            switch (viewType)
            {
                case ViewType.Login:
                    return _createLoginViewModel();
                case ViewType.Home:
                    return _createHomeViewModel();
                case ViewType.User:
                    return _createUserViewModel();
                default:
                    throw new ArgumentOutOfRangeException(nameof(viewType), viewType, null);
            }
        }
    }
}