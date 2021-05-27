using System.IO;
using System.Threading.Tasks;
using BookeryApi.Services.Storage;
using Microsoft.Win32;
using WPF.Controls;

namespace WPF.Commands
{
    internal class DownloadFileCommand : AsyncCommand
    {
        private readonly IItemService _itemService;

        public DownloadFileCommand(IItemService itemService)
        {
            _itemService = itemService;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            var itemControl = parameter as ItemControl;
            if (itemControl is null)
                return;

            var item = itemControl.Item;
            if (item.IsDirectory)
                return;

            var stream = await _itemService.DownloadFile(item.Path);
            byte[] bytes;
            await using (var memoryStream = new MemoryStream())
            {
                await stream.CopyToAsync(memoryStream);
                bytes = memoryStream.ToArray();
            }

            var saveFileDialog = new SaveFileDialog();
            saveFileDialog.FileName = item.Name;
            if (saveFileDialog.ShowDialog() == true) await File.WriteAllBytesAsync(saveFileDialog.FileName, bytes);
        }
    }
}