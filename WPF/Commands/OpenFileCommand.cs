using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using BookeryApi.Services.Storage;
using Domain.Models;

namespace WPF.Commands
{
    internal class OpenFileCommand : AsyncCommand
    {
        private readonly IItemService _itemService;

        public OpenFileCommand(IItemService itemService)
        {
            _itemService = itemService;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (parameter is Item item)
            {
                if (item.IsDirectory)
                {
                    return;
                }

                var stream = await _itemService.DownloadFile(item.Path);
                byte[] bytes;
                await using (var memoryStream = new MemoryStream())
                {
                    await stream.CopyToAsync(memoryStream);
                    bytes = memoryStream.ToArray();
                }

                var path = Path.Combine(Environment.CurrentDirectory, "temp", item.Name);

                await File.WriteAllBytesAsync(path, bytes);

                new Process
                {
                    StartInfo = new ProcessStartInfo(path)
                    {
                        UseShellExecute = true
                    }
                }.Start();
            }
        }
    }
}