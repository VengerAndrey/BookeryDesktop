using System;
using System.Threading.Tasks;
using BookeryApi.Services.Storage;
using WPF.ViewModels;

namespace WPF.Commands
{
    class RenameShareCommand : AsyncCommand
    {
        private readonly SharesViewModel _sharesViewModel;
        private readonly IShareService _shareService;
        private readonly Action _callback;

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
                _sharesViewModel.ContextMenuShare.Name = name;
                await _shareService.Update(_sharesViewModel.ContextMenuShare);
                _callback();
            }
        }
    }
}
