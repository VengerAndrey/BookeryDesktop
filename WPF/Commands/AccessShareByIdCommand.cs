using System;
using System.Threading.Tasks;
using BookeryApi.Exceptions;
using BookeryApi.Services.User;
using WPF.ViewModels;

namespace WPF.Commands
{
    internal class AccessShareByIdCommand : AsyncCommand
    {
        private readonly IAccessService _accessService;
        private readonly Action _callback;
        private readonly SharesViewModel _sharesViewModel;

        public AccessShareByIdCommand(IAccessService accessService, SharesViewModel sharesViewModel, Action callback)
        {
            _accessService = accessService;
            _sharesViewModel = sharesViewModel;
            _callback = callback;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (parameter is string id)
            {
                try
                {
                    await _accessService.AccessById(Guid.Parse(id));
                    _callback();
                }
                catch (DataNotFoundException e)
                {
                    _sharesViewModel.MessageViewModel.Message = e.Message;
                }
            }
        }
    }
}