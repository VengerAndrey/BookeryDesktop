﻿using System.IO;
using System.Threading.Tasks;
using BookeryApi.Services.Storage;
using Domain.Models;
using Microsoft.Win32;

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

                var extension = Path.GetExtension(item.Path);
                var saveFileDialog = new SaveFileDialog();
                saveFileDialog.FileName = item.Name;
                saveFileDialog.DefaultExt = extension ?? "";
                saveFileDialog.Filter = "File|*" + (extension ?? "*") + "|All files|*.*";
                if (saveFileDialog.ShowDialog() == true)
                {
                    await File.WriteAllBytesAsync(saveFileDialog.FileName, bytes);
                }
            }
        }
    }
}