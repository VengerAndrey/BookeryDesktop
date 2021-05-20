using System.Threading.Tasks;
using BookeryApi.Services;
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
            var shares = await _shareService.GetAll();

            _homeViewModel.Shares = shares;
        }
    }
}