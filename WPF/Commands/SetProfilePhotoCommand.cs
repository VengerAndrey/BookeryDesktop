using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using BookeryApi.Services.User;
using Microsoft.Win32;

namespace WPF.Commands
{
    internal class SetProfilePhotoCommand : AsyncCommand
    {
        private readonly Action _callback;
        private readonly IPhotoService _photoService;

        public SetProfilePhotoCommand(IPhotoService photoService, Action callback)
        {
            _photoService = photoService;
            _callback = callback;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            var openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png)|*.jpg;*.jpeg;*.jpe;*.jfif;*.png";
            if (openFileDialog.ShowDialog() == true)
            {
                await using var stream = File.OpenRead(openFileDialog.FileName);
                var streamContent = new StreamContent(stream);
                var fileName = Path.GetFileName(openFileDialog.FileName);
                var multipartFormDataContent = new MultipartFormDataContent
                {
                    {streamContent, "file", fileName}
                };

                await _photoService.Set(multipartFormDataContent);

                _callback();
            }
        }
    }
}