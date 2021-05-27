using System.Collections.Generic;
using System.Windows.Input;
using BookeryApi.Services.Storage;
using Domain.Models;
using WPF.Commands;
using WPF.Common;
using WPF.Controls;

namespace WPF.ViewModels
{
    internal class HomeViewModel : BaseViewModel
    {
        private readonly ICommand _commandDownloadItem;

        private List<ItemControl> _itemControls;

        private IEnumerable<Item> _items;
        private IEnumerable<Share> _shares;

        public HomeViewModel(IShareService shareService, IItemService itemService)
        {
            _commandDownloadItem = new DownloadItemCommand(itemService);

            MessageViewModel = new MessageViewModel();

            ItemsContextMenuItems = new List<ContextMenuItemViewModel>();
            ItemsContextMenuItems.Add(new ContextMenuItemViewModel
            {
                Header = "Download", Command = _commandDownloadItem,
                Image = ContextMenuItemIconHelper.GetImage(ContextMenuIconName.Download)
            });

            LoadSharesCommand = new LoadSharesCommand(this, shareService);
            //LoadSharesCommand.Execute(null);

            LoadItemsCommand = new LoadItemsCommand(this, itemService);
        }

        public MessageViewModel MessageViewModel { get; }

        public IEnumerable<Share> Shares
        {
            get => _shares;
            set
            {
                _shares = value;
                OnPropertyChanged(nameof(Shares));
            }
        }

        public ICommand LoadSharesCommand { get; }

        public IEnumerable<Item> Items
        {
            get => _items;
            set
            {
                _items = value;
                OnPropertyChanged(nameof(Items));
            }
        }

        public List<ItemControl> ItemControls
        {
            get => _itemControls;
            set
            {
                _itemControls = value;
                OnPropertyChanged(nameof(ItemControls));
            }
        }

        public ICommand LoadItemsCommand { get; }

        public List<ContextMenuItemViewModel> ItemsContextMenuItems { get; set; }

        public void Reset()
        {
            if (_itemControls != null && _itemControls.Count > 0)
                _itemControls.Clear();
            _shares = null;
        }
    }
}