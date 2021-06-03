using System;
using System.Threading.Tasks;
using BookeryApi.Exceptions;
using BookeryApi.Services.Storage;
using Domain.Models;
using WPF.ViewModels;

namespace WPF.Commands
{
    internal class DeleteShareCommand : AsyncCommand
    {
        private readonly Action<Share> _callback;
        private readonly HomeViewModel _homeViewModel;
        private readonly IShareService _shareService;

        public DeleteShareCommand(HomeViewModel homeViewModel, IShareService shareService, Action<Share> callback)
        {
            _homeViewModel = homeViewModel;
            _shareService = shareService;
            _callback = callback;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (parameter is Share share)
            {
                try
                {
                    await _shareService.Delete(share.Id);
                }
                catch (ForbiddenException e)
                {
                    _homeViewModel.MessageViewModel.Message = e.Message;
                }

                _callback(share);
            }
        }
    }
}