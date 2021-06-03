using System;
using System.Threading.Tasks;
using BookeryApi.Services.Storage;
using Domain.Models;
using WPF.ViewModels;

namespace WPF.Commands
{
    internal class CreateCommand : AsyncCommand
    {
        private readonly Action _callback;
        private readonly HomeViewModel _homeViewModel;
        private readonly IItemService _itemService;
        private readonly IShareService _shareService;

        private Item _lastItem;

        public CreateCommand(HomeViewModel homeViewModel, IShareService shareService, IItemService itemService,
            Action callback)
        {
            _homeViewModel = homeViewModel;
            _shareService = shareService;
            _itemService = itemService;
            _callback = callback;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            switch (_homeViewModel.DataInputViewModel.DataInputType)
            {
                case DataInputType.ShareName:
                {
                    if (parameter is string shareName)
                    {
                        await _shareService.Create(shareName);
                        _homeViewModel.DataInputViewModel.CancelCommand.Execute(null);

                        _callback();
                    }
                }
                    break;
                case DataInputType.DirectoryName:
                {
                    if (parameter is Item item)
                    {
                        _lastItem = item;
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
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}