using System;
using System.Threading.Tasks;
using BookeryApi.Exceptions;
using BookeryApi.Services.Storage;
using WPF.ViewModels;

namespace WPF.Commands
{
    internal class RenameShareCommand : AsyncCommand
    {
        private readonly Action _callback;
        private readonly IShareService _shareService;
        private readonly SharesViewModel _sharesViewModel;

        public RenameShareCommand(SharesViewModel sharesViewModel, IShareService shareService, Action callback)
        {
            _sharesViewModel = sharesViewModel;
            _shareService = shareService;
            _callback = callback;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (parameter is string name)
            {
                try
                {
                    _sharesViewModel.ContextMenuShare.Name = name;
                    await _shareService.Update(_sharesViewModel.ContextMenuShare);
                    _callback();
                }
                catch (ForbiddenException e)
                {
                    _sharesViewModel.MessageViewModel.Message = e.Message;
                }
            }
        }
    }
}