using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using BookeryApi.Services.User;
using WPF.ViewModels;

namespace WPF.Commands
{
    internal class LoadProfilePhotoCommand : AsyncCommand
    {
        private readonly IPhotoService _photoService;
        private readonly UserViewModel _userViewModel;

        public LoadProfilePhotoCommand(UserViewModel userViewModel, IPhotoService photoService)
        {
            _userViewModel = userViewModel;
            _photoService = photoService;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            try
            {
                var content = await _photoService.Get();
                if (content is null)
                {
                    return;
                }

                await using var stream = new MemoryStream();
                var image = new BitmapImage();
                image.BeginInit();
                image.CacheOption = BitmapCacheOption.OnLoad;
                await content.CopyToAsync(stream);
                image.StreamSource = stream;
                stream.Position = 0;
                image.EndInit();
                _userViewModel.Image = image;
            }
            catch (Exception)
            {
                _userViewModel.MessageViewModel.Message = "Remote service is not available.";
            }
        }
    }
}