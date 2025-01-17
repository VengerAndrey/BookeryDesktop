﻿using System;
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
            IItemService itemService, ICommand openDataInputCommand)
        {
            MessageViewModel = messageViewModel;
            DataInputViewModel = dataInputViewModel;

            OpenDataInputCommand = openDataInputCommand;

            var callback = new Action(() =>
            {
                LoadItemsCommand.Execute(CurrentItem.Path);
                DataInputViewModel.CancelCommand.Execute(null);
            });

            LoadItemsCommand = new LoadItemsCommand(this, itemService);
            CreateDirectoryCommand = new CreateDirectoryCommand(this, itemService, callback);
            RenameFileCommand = new RenameFileCommand(this, itemService, callback);
            DeleteItemCommand = new DeleteItemCommand(itemService, () => LoadItemsCommand.Execute(CurrentItem.Path));
            DownloadFileCommand = new DownloadFileCommand(itemService);
            UploadFileCommand = new UploadFileCommand(itemService, () => LoadItemsCommand.Execute(CurrentItem.Path));
            OpenFileCommand = new OpenFileCommand(itemService);
            UpdateCurrentItemCommand = new UpdateCurrentItemCommand(this);
            UpdateContextMenuItemCommand = new UpdateContextMenuItemCommand(this);

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

        public Item ContextMenuItem { get; set; }

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
        public ICommand RenameFileCommand { get; }
        public ICommand DeleteItemCommand { get; }
        public ICommand DownloadFileCommand { get; }
        public ICommand UploadFileCommand { get; }
        public ICommand OpenFileCommand { get; }
        public ICommand UpdateCurrentItemCommand { get; }
        public ICommand UpdateContextMenuItemCommand { get; }

        public ICommand OpenDataInputCommand { get; }

        public ContextMenu ListBoxItemsContextMenu { get; private set; }

        public event Action CurrentItemChanged;

        protected virtual void OnCurrentItemChanged()
        {
            CurrentItemChanged?.Invoke();
        }
    }
}