using System;
using System.Threading.Tasks;
using BookeryApi.Services.Storage;
using WPF.ViewModels;

namespace WPF.Commands
{
    internal class CreateDirectoryCommand : AsyncCommand
    {
        private readonly Action _callback;
        private readonly IItemService _itemService;
        private readonly ItemsViewModel _itemsViewModel;

        public CreateDirectoryCommand(ItemsViewModel itemsViewModel, IItemService itemService, Action callback)
        {
            _itemsViewModel = itemsViewModel;
            _itemService = itemService;
            _callback = callback;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (parameter is string name)
            {
                await _itemService.CreateDirectory(_itemsViewModel.CurrentItem.Path + "/" + name);
                _callback();
            }
        }
    }
}