using System;
using WPF.State.Navigators;

namespace WPF.ViewModels.Factories
{
    internal class ViewModelFactory : IViewModelFactory
    {
        private readonly CreateViewModel<ItemsViewModel> _createItemsViewModel;
        private readonly CreateViewModel<MockViewModel> _createMockViewModel;

        public ViewModelFactory(CreateViewModel<ItemsViewModel> createItemsViewModel,
            CreateViewModel<MockViewModel> createMockViewModel)
        {
            _createItemsViewModel = createItemsViewModel;
            _createMockViewModel = createMockViewModel;
        }

        public BaseViewModel CreateViewModel(ViewType viewType)
        {
            switch (viewType)
            {
                case ViewType.Files:
                    return _createItemsViewModel();
                case ViewType.Mock:
                    return _createMockViewModel();
                default:
                    throw new ArgumentOutOfRangeException(nameof(viewType), viewType, null);
            }
        }
    }
}