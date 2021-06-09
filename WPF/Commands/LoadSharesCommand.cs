using System;
using System.Threading.Tasks;
using BookeryApi.Services.Storage;
using WPF.ViewModels;

namespace WPF.Commands
{
    internal class LoadSharesCommand : AsyncCommand
    {
        private readonly IShareService _shareService;
        private readonly SharesViewModel _sharesViewModel;

        public LoadSharesCommand(SharesViewModel sharesViewModel, IShareService shareService)
        {
            _sharesViewModel = sharesViewModel;
            _shareService = shareService;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            try
            {
                _sharesViewModel.Shares = await _shareService.GetAll();
            }
            catch (Exception)
            {
                _sharesViewModel.MessageViewModel.Message = "Remote service is not available.";
            }
        }
    }
}