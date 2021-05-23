using System;
using System.Threading.Tasks;
using BookeryApi.Exceptions;
using BookeryApi.Services.User;
using WPF.ViewModels;

namespace WPF.Commands
{
    internal class LoadUserCommand : AsyncCommand
    {
        private readonly IUserService _userService;
        private readonly UserViewModel _userViewModel;

        public LoadUserCommand(UserViewModel userViewModel, IUserService userService)
        {
            _userViewModel = userViewModel;
            _userService = userService;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            try
            {
                _userViewModel.User = await _userService.Get();
            }
            catch (DataNotFoundException e)
            {
                _userViewModel.MessageViewModel.Message = e.Message;
            }
            catch (Exception)
            {
                _userViewModel.MessageViewModel.Message = "Remote service is not available.";
            }
        }
    }
}