using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Input;
using BookeryApi.Services.Storage;
using Domain.Models;
using WPF.Commands;
using WPF.Common.ContextMenus;
using WPF.Controls;

namespace WPF.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        private Item _currentItem;
        private List<ItemControl> _itemControls;

        private IEnumerable<Share> _shares;

        public HomeViewModel(IShareService shareService, IItemService itemService)
        {
            MessageViewModel = new MessageViewModel();
            DataInputViewModel = new DataInputViewModel();

            LoadSharesCommand = new LoadSharesCommand(this, shareService);

            LoadItemsCommand = new LoadItemsCommand(this, itemService);
            RefreshItemsCommand = new RefreshItemsCommand(this, itemService);
            DownloadFileCommand = new DownloadFileCommand(itemService);
            UploadFileCommand = new UploadCommand(itemService, () => { RefreshItemsCommand.Execute(CurrentItem); });
            CreateDirectoryCommand =
                new CreateDirectoryCommand(this, itemService, () => { RefreshItemsCommand.Execute(CurrentItem); });
            DeleteItemCommand = new DeleteItemCommand(itemService, () => { RefreshItemsCommand.Execute(CurrentItem); });

            ListBoxItemsContextMenu = new ListBoxItemsContextMenu(this);
        }

        public Item CurrentItem
        {
            get => _currentItem;
            set
            {
                ParentItem = _currentItem;
                _currentItem = value;
                ListBoxItemsContextMenu = new ListBoxItemsContextMenu(this);
                OnCurrentItemChanged();
            }
        }

        public Item ParentItem { get; set; }

        public MessageViewModel MessageViewModel { get; }
        public DataInputViewModel DataInputViewModel { get; }

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
        public ICommand RefreshItemsCommand { get; }

        public ICommand DownloadFileCommand { get; }
        public ICommand UploadFileCommand { get; }
        public ICommand CreateDirectoryCommand { get; }
        public ICommand DeleteItemCommand { get; }

        public ContextMenu ListBoxItemsContextMenu { get; private set; }

        public event Action CurrentItemChanged;

        public void Reset()
        {
            if (_itemControls != null && _itemControls.Count > 0)
                _itemControls.Clear();
            _shares = null;
        }

        protected virtual void OnCurrentItemChanged()
        {
            CurrentItemChanged?.Invoke();
        }
    }
}