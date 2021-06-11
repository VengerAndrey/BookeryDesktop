using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BookeryApi.Services.Storage;
using Domain.Models;
using WPF.Common;
using WPF.Common.ContextMenus;
using WPF.Controls;
using WPF.ViewModels;

namespace WPF.Commands
{
    internal class LoadItemsCommand : AsyncCommand
    {
        private readonly IItemService _itemService;
        private readonly ItemsViewModel _itemsViewModel;
        private readonly PathBuilder _pathBuilder;

        public LoadItemsCommand(ItemsViewModel itemsViewModel, IItemService itemService)
        {
            _itemsViewModel = itemsViewModel;
            _itemService = itemService;
            _pathBuilder = new PathBuilder();
        }

        public override async Task ExecuteAsync(object parameter)
        {
            var itemControls = new List<ItemControl>();

            if (parameter is string path)
            {
                _pathBuilder.ParsePath(path);

                if (_pathBuilder.IsFile())
                {
                    return;
                }

                if (_pathBuilder.GetDepth(_pathBuilder.GetPath()) > 2)
                {
                    var tempPath = _pathBuilder.GetPath();
                    _pathBuilder.GetLastNode();
                    itemControls.Add(new ItemControl(new Item
                        {
                            Name = "[..]",
                            IsDirectory = true,
                            Size = null,
                            Path = _pathBuilder.GetPath()
                        }, this, _itemsViewModel.OpenFileCommand,
                        _itemsViewModel.UpdateCurrentItemCommand, _itemsViewModel.UpdateContextMenuItemCommand));
                    _pathBuilder.ParsePath(tempPath);
                }

                try
                {
                    var subItems = await _itemService.GetSubItems(_pathBuilder.GetPath());

                    if (subItems != null)
                    {
                        foreach (var subItem in subItems)
                        {
                            var itemControl = new ItemControl(subItem, this, _itemsViewModel.OpenFileCommand,
                                _itemsViewModel.UpdateCurrentItemCommand, _itemsViewModel.UpdateContextMenuItemCommand);
                            if (subItem.IsDirectory)
                            {
                                itemControl.ContextMenu = new DirectoryContextMenu(_itemsViewModel, itemControl);
                            }
                            else
                            {
                                itemControl.ContextMenu = new FileContextMenu(_itemsViewModel, itemControl);
                            }

                            itemControls.Add(itemControl);
                        }
                    }
                }
                catch (Exception)
                {
                    _itemsViewModel.MessageViewModel.Message = "Remote service is not available.";
                }
            }

            _itemsViewModel.ItemControls = itemControls;
        }
    }
}