using System.Threading.Tasks;
using WPF.ViewModels;

namespace WPF.Commands
{
    internal class CreateShareCommand : AsyncCommand
    {
        private readonly HomeViewModel _homeViewModel;

        public CreateShareCommand(HomeViewModel homeViewModel)
        {
            _homeViewModel = homeViewModel;
        }

        public override Task ExecuteAsync(object parameter)
        {
            _homeViewModel.DataInputViewModel.Show(DataInputType.ShareName);
            return Task.CompletedTask;
        }
    }
}