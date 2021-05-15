using System.Threading.Tasks;
using BookeryApi.Services;
using WPF.ViewModels;

namespace WPF.Commands
{
    internal class LoadSharesCommand : AsyncCommand
    {
        private readonly ItemsViewModel _itemsViewModel;
        private readonly IStorageService _storageService;

        public LoadSharesCommand(ItemsViewModel itemsViewModel, IStorageService storageService)
        {
            _itemsViewModel = itemsViewModel;
            _storageService = storageService;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            var shares = await _storageService.GetAllShares();

            _itemsViewModel.Shares = shares;
        }
    }
}