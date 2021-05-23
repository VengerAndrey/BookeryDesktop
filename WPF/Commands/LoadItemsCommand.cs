using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using BookeryApi.Services.Storage;
using Domain.Models;
using WPF.Common;
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

            if (parameter is string path) _pathBuilder.ParsePath(path);

            if (parameter is ItemControl itemControl)
            {
                if (itemControl.Item.Name == "[..]")
                    _pathBuilder.GetLastNode();
                else
                    _pathBuilder.AddNode(itemControl.Item.Name);

                if (_pathBuilder.IsFile())
                {
                    MessageBox.Show("This is a file.");
                    _pathBuilder.GetLastNode();
                    return;
                }

                if (_pathBuilder.GetDepth(_pathBuilder.GetPath()) > 2)
                    itemControls.Add(new ItemControl(new Item {Name = "[..]", IsDirectory = true}, this));
            }

            try
            {
                var items = await _itemService.GetSubItems(_pathBuilder.GetPath());

                foreach (var item in items) itemControls.Add(new ItemControl(item, this));

                _homeViewModel.ItemControls = itemControls;
            }
            catch (Exception)
            {
                _homeViewModel.MessageViewModel.Message = "Remote service is not available.";
            }
        }

        /*public override async Task ExecuteAsync(object parameter)
        {
            var items = new List<Item>();

            if (parameter is string path) _pathBuilder.ParsePath(path);

            if (parameter is Item item)
            {
                if (item.Name == "[..]")
                {
                    _pathBuilder.GetLastNode();
                }
                else
                {
                    _pathBuilder.AddNode(item.Name);
                }

                if (_pathBuilder.IsFile())
                {
                    MessageBox.Show("This is a file.");
                    _pathBuilder.GetLastNode();
                    return;
                }

                if (_pathBuilder.GetDepth(_pathBuilder.GetPath()) > 2)
                    items.Add(new Item { Name = "[..]" });
            }

            var subItems = await _itemService.GetSubItems(_pathBuilder.GetPath());

            foreach (var subItem in subItems) items.Add(subItem);

            _homeViewModel.Items = items;
        }*/
    }
}