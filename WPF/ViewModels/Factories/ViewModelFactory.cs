using System;
using WPF.State.Navigators;

namespace WPF.ViewModels.Factories
{
    internal class ViewModelFactory : IViewModelFactory
    {
        private readonly CreateViewModel<MockViewModel> _createMockViewModel;
        private readonly CreateViewModel<HomeViewModel> _createHomeViewModel;

        public ViewModelFactory(CreateViewModel<MockViewModel> createMockViewModel, 
            CreateViewModel<HomeViewModel> createHomeViewModel)
        {
            _createMockViewModel = createMockViewModel;
            _createHomeViewModel = createHomeViewModel;
        }

        public BaseViewModel CreateViewModel(ViewType viewType)
        {
            switch (viewType)
            {
                case ViewType.Home:
                    return _createHomeViewModel();
                case ViewType.Mock:
                    return _createMockViewModel();
                default:
                    throw new ArgumentOutOfRangeException(nameof(viewType), viewType, null);
            }
        }
    }
}