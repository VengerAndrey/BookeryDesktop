using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Input;
using BookeryApi.Services.Storage;
using Domain.Models;
using WPF.Commands;
using WPF.Common;
using WPF.Common.ContextMenus;
using WPF.Controls;

namespace WPF.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        private List<ItemControl> _itemControls;
        private Item _currentItem;

        private IEnumerable<Share> _shares;

        public HomeViewModel(IShareService shareService, IItemService itemService)
        {
            MessageViewModel = new MessageViewModel();

            LoadSharesCommand = new LoadSharesCommand(this, shareService);
            //LoadSharesCommand.Execute(null);

            LoadItemsCommand = new LoadItemsCommand(this, itemService);
            DownloadFileCommand = new DownloadFileCommand(itemService);
            UploadFileCommand = new UploadCommand(itemService);
            CreateDirectoryCommand = new CreateDirectoryCommand(itemService);
            DeleteItemCommand = new DeleteItemCommand(itemService);

            ListBoxItemsContextMenu = new ListBoxItemsContextMenu(this);
        }

        public event Action CurrentItemChanged;

        public Item CurrentItem
        {
            get => _currentItem;
            set
            {
                _currentItem = value;
                ListBoxItemsContextMenu = new ListBoxItemsContextMenu(this);
                OnCurrentItemChanged();
            }
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

        public ICommand DownloadFileCommand { get; }
        public ICommand UploadFileCommand { get; }
        public ICommand CreateDirectoryCommand { get; }
        public ICommand DeleteItemCommand { get; }

        public ContextMenu ListBoxItemsContextMenu { get; private set; }

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