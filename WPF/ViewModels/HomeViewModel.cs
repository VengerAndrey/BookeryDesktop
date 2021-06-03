using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Input;
using BookeryApi.Services.Storage;
using BookeryApi.Services.User;
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

        public HomeViewModel(IShareService shareService, IItemService itemService, IAccessService accessService)
        {
            MessageViewModel = new MessageViewModel();
            DataInputViewModel = new DataInputViewModel();

            LoadSharesCommand = new LoadSharesCommand(this, shareService);
            LoadItemsCommand = new LoadItemsCommand(this, itemService);
            RefreshItemsCommand = new RefreshItemsCommand(this, itemService);

            DataInputCommand =
                new DataInputCommand(this, shareService, itemService, accessService, () =>
                {
                    LoadSharesCommand.Execute(null);
                    RefreshItemsCommand.Execute(CurrentItem);
                });
            CreateShareCommand = new CreateShareCommand(this);
            CreateDirectoryCommand = new CreateDirectoryCommand(this);

            DeleteShareCommand = new DeleteShareCommand(this, shareService, share =>
            {
                LoadSharesCommand.Execute(null);
                if (share?.Id == CurrentShare?.Id)
                {
                    LoadItemsCommand.Execute(null);
                }
            });
            DeleteItemCommand = new DeleteItemCommand(itemService, () => RefreshItemsCommand.Execute(CurrentItem));

            DownloadFileCommand = new DownloadFileCommand(itemService);
            UploadFileCommand = new UploadCommand(itemService, () => RefreshItemsCommand.Execute(CurrentItem));

            AccessShareByIdCommand = new AccessShareByIdCommand(this);

            ListBoxItemsContextMenu = new ListBoxItemsContextMenu(this);
            ListBoxSharesContextMenu = new ListBoxSharesContextMenu(this);
        }

        public Share CurrentShare { get; set; }

        public Item ParentItem { get; set; }

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

        public IEnumerable<Share> Shares
        {
            get => _shares;
            set
            {
                _shares = value;
                OnPropertyChanged(nameof(Shares));
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

        public ICommand LoadSharesCommand { get; }
        public ICommand LoadItemsCommand { get; }
        public ICommand RefreshItemsCommand { get; }

        public ICommand DataInputCommand { get; }
        public ICommand CreateShareCommand { get; }
        public ICommand CreateDirectoryCommand { get; }

        public ICommand DeleteShareCommand { get; }
        public ICommand DeleteItemCommand { get; }

        public ICommand DownloadFileCommand { get; }
        public ICommand UploadFileCommand { get; }

        public ICommand AccessShareByIdCommand { get; }

        public ContextMenu ListBoxItemsContextMenu { get; private set; }
        public ContextMenu ListBoxSharesContextMenu { get; }

        public MessageViewModel MessageViewModel { get; }
        public DataInputViewModel DataInputViewModel { get; }

        public event Action CurrentItemChanged;

        public void Reset()
        {
            if (_itemControls != null && _itemControls.Count > 0)
            {
                _itemControls.Clear();
            }

            _shares = null;
        }

        protected virtual void OnCurrentItemChanged()
        {
            CurrentItemChanged?.Invoke();
        }
    }
}