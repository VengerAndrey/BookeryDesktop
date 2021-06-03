using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using BookeryApi.Services.Storage;
using Domain.Models;
using Microsoft.Win32;

namespace WPF.Commands
{
    internal class UploadCommand : AsyncCommand
    {
        private readonly Action _callback;
        private readonly IItemService _itemService;

        public UploadCommand(IItemService itemService, Action callback)
        {
            _itemService = itemService;
            _callback = callback;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            var item = parameter as Item;
            if (item is null)
            {
                return;
            }

            var openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                await using var stream = File.OpenRead(openFileDialog.FileName);
                var streamContent = new StreamContent(stream);
                var fileName = Path.GetFileName(openFileDialog.FileName);
                var multipartFormDataContent = new MultipartFormDataContent
                {
                    {streamContent, "file", fileName}
                };

                await _itemService.UploadFile(item.Path + "/" + fileName, multipartFormDataContent);

                _callback();
            }
        }
    }
}