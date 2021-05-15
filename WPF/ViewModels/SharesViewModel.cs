using System.Collections.Generic;
using System.Windows.Input;
using BookeryApi.Services;
using Domain.Models;

namespace WPF.ViewModels
{
    internal class SharesViewModel : BaseViewModel
    {
        private List<Share> _shares;

        public SharesViewModel(IStorageService storageService)
        {
            //LoadSharesCommand = new LoadSharesCommand(this, storageService);
            LoadSharesCommand.Execute(null);
        }

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