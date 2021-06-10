using System.Windows.Input;
using System.Windows.Media.Imaging;
using BookeryApi.Services.User;
using Domain.Models;
using WPF.Commands;
using WPF.State.Authentication;
using WPF.State.Navigation;

namespace WPF.ViewModels
{
    internal class UserViewModel : BaseViewModel
    {
        private User _user;
        private BitmapImage _image;

        public UserViewModel(IAuthenticator authenticator, IRenavigator loginRenavigator, IUserService userService,
            IPhotoService photoService)
        {
            MessageViewModel = new MessageViewModel();

            LoadUserCommand = new LoadUserCommand(this, userService);
            LoadUserCommand.Execute(null);

            LoadProfilePhotoCommand = new LoadProfilePhotoCommand(this, photoService);
            LoadProfilePhotoCommand.Execute(null);

            LogoutCommand = new LogoutCommand(authenticator, loginRenavigator);
        }

        public User User
        {
            get => _user;
            set
            {
                _user = value;
                OnPropertyChanged(nameof(User));
            }
        }

        public BitmapImage Image
        {
            get => _image;
            set
            {
                _image = value;
                OnPropertyChanged(nameof(Image));
            }
        }

        public MessageViewModel MessageViewModel { get; }

        public ICommand LoadUserCommand { get; }
        public ICommand LoadProfilePhotoCommand { get; }
        public ICommand LogoutCommand { get; }
    }
}