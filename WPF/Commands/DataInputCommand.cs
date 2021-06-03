using System;
using System.Threading.Tasks;
using BookeryApi.Exceptions;
using BookeryApi.Services.Storage;
using BookeryApi.Services.User;
using Domain.Models;
using WPF.ViewModels;

namespace WPF.Commands
{
    internal class DataInputCommand : AsyncCommand
    {
        private readonly IAccessService _accessService;
        private readonly Action _callback;
        private readonly HomeViewModel _homeViewModel;
        private readonly IItemService _itemService;
        private readonly IShareService _shareService;

        private Item _lastItem;

        public DataInputCommand(HomeViewModel homeViewModel, IShareService shareService, IItemService itemService,
            IAccessService accessService, Action callback)
        {
            _homeViewModel = homeViewModel;
            _shareService = shareService;
            _itemService = itemService;
            _callback = callback;
            _accessService = accessService;
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
                case DataInputType.ShareId:
                {
                    if (parameter is null)
                    {
                        _homeViewModel.DataInputViewModel.Show(DataInputType.ShareId);
                    }
                    else if (parameter is string id)
                    {
                        try
                        {
                            await _accessService.AccessById(Guid.Parse(id));
                        }
                        catch (DataNotFoundException e)
                        {
                            _homeViewModel.MessageViewModel.Message = e.Message;
                        }

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