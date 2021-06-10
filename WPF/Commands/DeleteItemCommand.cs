using System;
using System.Threading.Tasks;
using System.Windows;
using BookeryApi.Services.Storage;
using Domain.Models;

namespace WPF.Commands
{
    internal class DeleteItemCommand : AsyncCommand
    {
        private readonly Action _callback;
        private readonly IItemService _itemService;

        public DeleteItemCommand(IItemService itemService, Action callback)
        {
            _itemService = itemService;
            _callback = callback;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (parameter is Item item)
            {
                var response = await _itemService.Delete(item.Path);

                if (response)
                {
                    _callback();
                }
                else
                {
                    MessageBox.Show($"Can't delete {item.Name}.");
                }
            }
        }
    }
}