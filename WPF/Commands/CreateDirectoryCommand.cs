using System;
using System.Threading.Tasks;
using BookeryApi.Services.Storage;
using Domain.Models;
using WPF.ViewModels;

namespace WPF.Commands
{
    internal class CreateDirectoryCommand : AsyncCommand
    {
        private readonly Action _callback;
        private readonly HomeViewModel _homeViewModel;
        private readonly IItemService _itemService;

        private Item _lastItem;

        public CreateDirectoryCommand(HomeViewModel homeViewModel, IItemService itemService, Action callback)
        {
            _homeViewModel = homeViewModel;
            _itemService = itemService;
            _callback = callback;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (parameter is Item item)
            {
                _lastItem = item;

                _homeViewModel.DataInputViewModel.Name = "Enter directory name:";
            }
            else if (parameter is string directoryName)
            {
                if (_lastItem is null)
                {
                    throw new Exception();
                }

                await _itemService.CreateDirectory(_lastItem.Path + "/" + directoryName);

                _lastItem = null;

                _homeViewModel.DataInputViewModel.CancelCommand.Execute(null);

                _callback();
            }
        }
    }
}