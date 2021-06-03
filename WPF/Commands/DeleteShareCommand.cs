using System;
using System.Threading.Tasks;
using BookeryApi.Services.Storage;
using Domain.Models;

namespace WPF.Commands
{
    internal class DeleteShareCommand : AsyncCommand
    {
        private readonly Action<Share> _callback;
        private readonly IShareService _shareService;

        public DeleteShareCommand(IShareService shareService, Action<Share> callback)
        {
            _shareService = shareService;
            _callback = callback;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (parameter is Share share)
            {
                await _shareService.Delete(share.Id);
                _callback(share);
            }
        }
    }
}