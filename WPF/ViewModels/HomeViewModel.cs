using System.Collections.Generic;
using System.Windows.Input;
using BookeryApi.Services;
using Domain.Models;
using WPF.Commands;
using WPF.Controls;

namespace WPF.ViewModels
{
    internal class HomeViewModel : BaseViewModel
    {
        private List<ItemControl> _itemControls;

        private IEnumerable<Item> _items;
        private IEnumerable<Share> _shares;

        public HomeViewModel(IShareService shareService, IItemService itemService)
        {
            LoadSharesCommand = new LoadSharesCommand(this, shareService);
            LoadSharesCommand.Execute(null);

            LoadItemsCommand = new LoadItemsCommand(this, itemService);
        }

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

        public IEnumerable<Item> Items
        {
            get => _items;
            set
            {
                _items = value;
                OnPropertyChanged(nameof(Items));
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
    }
}