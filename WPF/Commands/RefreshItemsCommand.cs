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
    internal class RefreshItemsCommand : AsyncCommand
    {
        private readonly HomeViewModel _homeViewModel;
        private readonly IItemService _itemService;
        private readonly PathBuilder _pathBuilder;

        public RefreshItemsCommand(HomeViewModel homeViewModel, IItemService itemService)
        {
            _homeViewModel = homeViewModel;
            _itemService = itemService;
            _pathBuilder = new PathBuilder();
        }

        public override async Task ExecuteAsync(object parameter)
        {
            var itemControls = new List<ItemControl>();

            if (parameter is string path) _pathBuilder.ParsePath(path);

            if (parameter is Item item)
            {
                _pathBuilder.ParsePath(item.Path);

                if (_pathBuilder.IsFile())
                {
                    MessageBox.Show("This is a file.");
                    return;
                }

                if (_pathBuilder.GetDepth(_pathBuilder.GetPath()) > 2)
                    itemControls.Add(new ItemControl(new Item
                    {
                        Name = "[..]",
                        IsDirectory = true,
                        Size = null,
                        Path = _pathBuilder.GetPath()
                    }, _homeViewModel.LoadItemsCommand, _homeViewModel));
            }

            try
            {
                var subItems = await _itemService.GetSubItems(_pathBuilder.GetPath());

                foreach (var subItem in subItems)
                {
                    var itemControl = new ItemControl(subItem, _homeViewModel.LoadItemsCommand, _homeViewModel);
                    if (subItem.IsDirectory)
                        itemControl.ContextMenu = new DirectoryContextMenu(_homeViewModel, itemControl);
                    else
                        itemControl.ContextMenu = new FileContextMenu(_homeViewModel, itemControl);

                    itemControls.Add(itemControl);
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