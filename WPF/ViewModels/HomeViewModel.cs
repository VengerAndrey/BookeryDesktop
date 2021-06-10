using System.Windows.Input;
using BookeryApi.Services.Storage;
using BookeryApi.Services.User;
using WPF.Commands;

namespace WPF.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        public HomeViewModel(IShareService shareService, IItemService itemService, IAccessService accessService)
        {
            MessageViewModel = new MessageViewModel();
            DataInputViewModel = new DataInputViewModel();

            OpenDataInputCommand = new OpenDataInputCommand(DataInputViewModel);

            ItemsViewModel =
                new ItemsViewModel(MessageViewModel, DataInputViewModel, itemService, OpenDataInputCommand);
            SharesViewModel = new SharesViewModel(MessageViewModel, DataInputViewModel, shareService, accessService,
                ItemsViewModel.LoadItemsCommand, OpenDataInputCommand);

            DataInputCommand = new UseDataInputValueCommand(DataInputViewModel, SharesViewModel.CreateShareCommand,
                ItemsViewModel.CreateDirectoryCommand, SharesViewModel.AccessShareByIdCommand, SharesViewModel.RenameShareCommand);
        }

        public SharesViewModel SharesViewModel { get; }
        public ItemsViewModel ItemsViewModel { get; }

        public ICommand OpenDataInputCommand { get; }
        public ICommand DataInputCommand { get; }

        public MessageViewModel MessageViewModel { get; }
        public DataInputViewModel DataInputViewModel { get; }

        public void Reset()
        {
            SharesViewModel.Shares = null;
            if (ItemsViewModel.ItemControls != null && ItemsViewModel.ItemControls.Count > 0)
            {
                ItemsViewModel.ItemControls.Clear();
            }
        }
    }
}