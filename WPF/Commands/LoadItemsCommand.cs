using System.Collections.Generic;
using System.Threading.Tasks;
using BookeryApi.Services;
using Domain.Models;
using WPF.Controls;
using WPF.ViewModels;

namespace WPF.Commands
{
    internal class LoadItemsCommand : AsyncCommand
    {
        private readonly ItemsViewModel _itemsViewModel;
        private readonly IStorageService _storageService;
        private string _path = "d4e21b92-8d47-4efd-8867-fe45fa18cac2/root";

        public LoadItemsCommand(ItemsViewModel itemsViewModel, IStorageService storageService)
        {
            _itemsViewModel = itemsViewModel;
            _storageService = storageService;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            var itemControls = new List<ItemControl>();

            if (parameter is string path) _path = path + "/root";

            if (parameter is null || parameter is string)
            {
                var allItems = await _storageService.GetSubItems(_path);

                foreach (var item in allItems) itemControls.Add(new ItemControl(item, this));

                _itemsViewModel.ItemControls = itemControls;

                return;
            }

            var itemControl = parameter as ItemControl;

            if (itemControl is null) return;

            itemControls.Add(new ItemControl(new Item {Name = "[..]"}, this));

            if (itemControl.Item.Name == "[..]")
                _path = _path.Substring(0, _path.LastIndexOf("/"));
            else
                _path = _path + "/" + itemControl.Item.Name;

            var items = await _storageService.GetSubItems(_path);

            foreach (var item in items) itemControls.Add(new ItemControl(item, this));

            _itemsViewModel.ItemControls = itemControls;
        }
    }
}