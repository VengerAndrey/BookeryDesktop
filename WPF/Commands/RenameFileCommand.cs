using System;
using System.IO;
using System.Threading.Tasks;
using BookeryApi.Services.Storage;
using WPF.ViewModels;

namespace WPF.Commands
{
    internal class RenameFileCommand : AsyncCommand
    {
        private readonly Action _callback;
        private readonly IItemService _itemService;
        private readonly ItemsViewModel _itemsViewModel;

        public RenameFileCommand(ItemsViewModel itemsViewModel, IItemService itemService, Action callback)
        {
            _itemsViewModel = itemsViewModel;
            _itemService = itemService;
            _callback = callback;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (parameter is string name)
            {
                if (name.Contains("."))
                {
                    name = name.Substring(0, name.IndexOf("."));
                }

                name += Path.GetExtension(_itemsViewModel.ContextMenuItem.Path);

                try
                {
                    await _itemService.RenameFile(_itemsViewModel.ContextMenuItem.Path, name);
                    _callback();
                }
                catch (Exception)
                {
                    _itemsViewModel.MessageViewModel.Message = "Remote service is not available.";
                }
            }
        }
    }
}