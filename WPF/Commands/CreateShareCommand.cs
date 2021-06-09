using System;
using System.Threading.Tasks;
using BookeryApi.Services.Storage;

namespace WPF.Commands
{
    internal class CreateShareCommand : AsyncCommand
    {
        private readonly Action _callback;
        private readonly IShareService _shareService;

        public CreateShareCommand(IShareService shareService, Action callback)
        {
            _shareService = shareService;
            _callback = callback;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (parameter is string name)
            {
                await _shareService.Create(name);
                _callback();
            }
        }
    }
}