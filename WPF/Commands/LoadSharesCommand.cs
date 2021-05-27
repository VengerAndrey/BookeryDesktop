using System;
using System.Threading.Tasks;
using BookeryApi.Services.Storage;
using WPF.ViewModels;

namespace WPF.Commands
{
    internal class LoadSharesCommand : AsyncCommand
    {
        private readonly HomeViewModel _homeViewModel;
        private readonly IShareService _shareService;

        public LoadSharesCommand(HomeViewModel homeViewModel, IShareService shareService)
        {
            _homeViewModel = homeViewModel;
            _shareService = shareService;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            try
            {
                _homeViewModel.Shares = await _shareService.GetAll();
            }
            catch (Exception)
            {
                _homeViewModel.MessageViewModel.Message = "Remote service is not available.";
            }
        }
    }
}