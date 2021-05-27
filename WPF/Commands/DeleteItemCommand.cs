using System.Threading.Tasks;
using System.Windows;
using BookeryApi.Services.Storage;
using WPF.Controls;

namespace WPF.Commands
{
    class DeleteItemCommand : AsyncCommand
    {
        private readonly IItemService _itemService;

        public DeleteItemCommand(IItemService itemService)
        {
            _itemService = itemService;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            var itemControl = parameter as ItemControl;
            if (itemControl is null)
                return;

            var item = itemControl.Item;

            var response = await _itemService.Delete(item.Path);

            if (response)
            {
                MessageBox.Show($"Successfully deleted {item.Name}.");
            }
            else
            {
                MessageBox.Show($"Can't delete {item.Name}.");
            }
        }
    }
}
