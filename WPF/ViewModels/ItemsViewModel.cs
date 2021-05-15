using System.Collections.Generic;
using System.Windows.Input;
using BookeryApi.Services;
using Domain.Models;
using WPF.Commands;
using WPF.Controls;

namespace WPF.ViewModels
{
    internal class ItemsViewModel : BaseViewModel
    {
        private List<ItemControl> _itemControls;

        private List<Share> _shares;

        public ItemsViewModel(IStorageService storageService)
        {
            LoadSharesCommand = new LoadSharesCommand(this, storageService);
            LoadSharesCommand.Execute(null);

            LoadItemsCommand = new LoadItemsCommand(this, storageService);
            LoadItemsCommand.Execute(null);
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

        public List<Share> Shares
        {
            get => _shares;
            set
            {
                _shares = value;
                OnPropertyChanged(nameof(Shares));
            }
        }

        public ICommand LoadSharesCommand { get; }
    }
}