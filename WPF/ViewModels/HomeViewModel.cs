using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using BookeryApi.Services;
using Domain.Models;
using WPF.Commands;
using WPF.Controls;

namespace WPF.ViewModels
{
    class HomeViewModel : BaseViewModel
    {
        private IEnumerable<Share> _shares;

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

        private IEnumerable<Item> _items;

        public IEnumerable<Item> Items
        {
            get => _items;
            set
            {
                _items = value;
                OnPropertyChanged(nameof(Items));
            }
        }

        private List<ItemControl> _itemControls;

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

        public HomeViewModel(IShareService shareService, IItemService itemService)
        {
            LoadSharesCommand = new LoadSharesCommand(this, shareService);
            LoadSharesCommand.Execute(null);

            LoadItemsCommand = new LoadItemsCommand(this, itemService);
        }
    }
}
