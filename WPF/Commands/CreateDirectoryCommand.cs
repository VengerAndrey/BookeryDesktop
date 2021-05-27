using System.Threading.Tasks;
using BookeryApi.Services.Storage;
using Domain.Models;

namespace WPF.Commands
{
    class CreateDirectoryCommand : AsyncCommand
    {
        private readonly IItemService _itemService;

        public CreateDirectoryCommand(IItemService itemService)
        {
            _itemService = itemService;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            var item = parameter as Item;
            if (item is null)
                return;

            await _itemService.CreateDirectory(item.Path + "/" + "New Folder");
        }
    }
}
