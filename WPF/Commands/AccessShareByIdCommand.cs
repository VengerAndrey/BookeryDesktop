using System.Threading.Tasks;
using WPF.ViewModels;

namespace WPF.Commands
{
    internal class AccessShareByIdCommand : AsyncCommand
    {
        private readonly HomeViewModel _homeViewModel;

        public AccessShareByIdCommand(HomeViewModel homeViewModel)
        {
            _homeViewModel = homeViewModel;
        }

        public override Task ExecuteAsync(object parameter)
        {
            _homeViewModel.DataInputViewModel.Show(DataInputType.ShareId);
            return Task.CompletedTask;
        }
    }
}