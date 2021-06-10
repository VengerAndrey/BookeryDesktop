using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using BookeryApi.Exceptions;
using BookeryApi.Services.User;
using WPF.ViewModels;

namespace WPF.Commands
{
    class LoadProfilePhotoCommand : AsyncCommand
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
                await using MemoryStream stream = new MemoryStream();
                var image = new BitmapImage();
                image.BeginInit();
                image.CacheOption = BitmapCacheOption.OnLoad;
                await content.CopyToAsync(stream);
                image.StreamSource = stream;
                stream.Position = 0;
                image.EndInit();
                _userViewModel.Image = image;
            }
            catch (DataNotFoundException e)
            {
                _userViewModel.MessageViewModel.Message = e.Message;
            }
            catch (Exception e)
            {
                _userViewModel.MessageViewModel.Message = "Remote service is not available.";
            }
        }
    }
}
