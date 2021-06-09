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
    public class ItemsViewModel : BaseViewModel
    {
        private Item _currentItem;
        private List<ItemControl> _itemControls;

        public ItemsViewModel(MessageViewModel messageViewModel, DataInputViewModel dataInputViewModel,
            IItemService itemService, ICommand dataInputCommand)
        {
            MessageViewModel = messageViewModel;
            DataInputViewModel = dataInputViewModel;

            DataInputCommand = dataInputCommand;

            LoadItemsCommand = new LoadItemsCommand(this, itemService);
            CreateDirectoryCommand = new CreateDirectoryCommand(this);
            DeleteItemCommand = new DeleteItemCommand(itemService, () => LoadItemsCommand.Execute(CurrentItem.Path));
            DownloadFileCommand = new DownloadFileCommand(itemService);
            UploadFileCommand = new UploadCommand(itemService, () => LoadItemsCommand.Execute(CurrentItem.Path));
            UpdateCurrentItemCommand = new UpdateCurrentItemCommand(this);

            ListBoxItemsContextMenu = new ListBoxItemsContextMenu(this);
        }

        public MessageViewModel MessageViewModel { get; }
        public DataInputViewModel DataInputViewModel { get; }

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
        public ICommand CreateDirectoryCommand { get; }
        public ICommand DeleteItemCommand { get; }
        public ICommand DownloadFileCommand { get; }
        public ICommand UploadFileCommand { get; }
        public ICommand UpdateCurrentItemCommand { get; }

        public ICommand DataInputCommand { get; }

        public ContextMenu ListBoxItemsContextMenu { get; private set; }

        public event Action CurrentItemChanged;

        protected virtual void OnCurrentItemChanged()
        {
            CurrentItemChanged?.Invoke();
        }
    }
}