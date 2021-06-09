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
        private readonly IShareService _shareService;
        private readonly SharesViewModel _sharesViewModel;

        public DeleteShareCommand(SharesViewModel sharesViewModel, IShareService shareService, Action<Share> callback)
        {
            _sharesViewModel = sharesViewModel;
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
                    _sharesViewModel.MessageViewModel.Message = e.Message;
                }

                _callback(share);
            }
        }
    }
}