using System;
using System.Threading.Tasks;
using BookeryApi.Services.Storage;
using Domain.Models;

namespace WPF.Commands
{
    internal class CreateDirectoryCommand : AsyncCommand
    {
        private readonly Action _callback;
        private readonly IItemService _itemService;

        public CreateDirectoryCommand(IItemService itemService, Action callback)
        {
            _itemService = itemService;
            _callback = callback;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            var item = parameter as Item;
            if (item is null)
                return;

            await _itemService.CreateDirectory(item.Path + "/" + "New Folder");

            _callback();
        }
    }
}