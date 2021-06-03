using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
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
        private readonly HomeViewModel _homeViewModel;
        private readonly IItemService _itemService;
        private readonly PathBuilder _pathBuilder;

        public LoadItemsCommand(HomeViewModel homeViewModel, IItemService itemService)
        {
            _homeViewModel = homeViewModel;
            _itemService = itemService;
            _pathBuilder = new PathBuilder();
        }

        public override async Task ExecuteAsync(object parameter)
        {
            var itemControls = new List<ItemControl>();

            if (parameter is string path)
            {
                _pathBuilder.ParsePath(path);
            }

            if (parameter is Item item)
            {
                if (item.Name == "[..]")
                {
                    _pathBuilder.GetLastNode();
                    _homeViewModel.CurrentItem = await _itemService.GetItem(_pathBuilder.GetPath());
                }
                else
                {
                    _pathBuilder.AddNode(item.Name);

                    if (_pathBuilder.IsFile())
                    {
                        MessageBox.Show("This is a file.");
                        return;
                    }

                    _homeViewModel.CurrentItem = item;
                }

                if (_pathBuilder.GetDepth(_pathBuilder.GetPath()) > 2)
                {
                    itemControls.Add(new ItemControl(new Item
                    {
                        Name = "[..]",
                        IsDirectory = true,
                        Size = null,
                        Path = _pathBuilder.GetPath()
                    }, this, _homeViewModel));
                }
            }

            try
            {
                var subItems = await _itemService.GetSubItems(_pathBuilder.GetPath());

                if (subItems != null)
                {
                    foreach (var subItem in subItems)
                    {
                        var itemControl = new ItemControl(subItem, this, _homeViewModel);
                        if (subItem.IsDirectory)
                        {
                            itemControl.ContextMenu = new DirectoryContextMenu(_homeViewModel, itemControl);
                        }
                        else
                        {
                            itemControl.ContextMenu = new FileContextMenu(_homeViewModel, itemControl);
                        }

                        itemControls.Add(itemControl);
                    }
                }

                _homeViewModel.ItemControls = itemControls;
            }
            catch (Exception)
            {
                _homeViewModel.MessageViewModel.Message = "Remote service is not available.";
            }
        }
    }
}