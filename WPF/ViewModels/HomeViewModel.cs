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

            DataInputCommand =
                new DataInputCommand(this, shareService, itemService, accessService, () =>
                {
                    SharesViewModel.LoadSharesCommand.Execute(null);
                    ItemsViewModel.LoadItemsCommand.Execute(ItemsViewModel.CurrentItem.Path);
                });

            ItemsViewModel = new ItemsViewModel(MessageViewModel, DataInputViewModel, itemService, DataInputCommand);
            SharesViewModel = new SharesViewModel(MessageViewModel, DataInputViewModel, shareService,
                ItemsViewModel.LoadItemsCommand);
        }

        public SharesViewModel SharesViewModel { get; }
        public ItemsViewModel ItemsViewModel { get; }

        public ICommand DataInputCommand { get; }

        public MessageViewModel MessageViewModel { get; }
        public DataInputViewModel DataInputViewModel { get; }

        public void Reset()
        {
            SharesViewModel.Shares = null;
            ItemsViewModel.ItemControls.Clear();
        }
    }
}