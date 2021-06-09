using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Input;
using BookeryApi.Services.Storage;
using Domain.Models;
using WPF.Commands;
using WPF.Common.ContextMenus;

namespace WPF.ViewModels
{
    public class SharesViewModel : BaseViewModel
    {
        private Share _currentShare;
        private IEnumerable<Share> _shares;

        public SharesViewModel(MessageViewModel messageViewModel, DataInputViewModel dataInputViewModel,
            IShareService shareService, ICommand loadItemsCommand)
        {
            MessageViewModel = messageViewModel;
            DataInputViewModel = dataInputViewModel;

            LoadSharesCommand = loadItemsCommand;

            LoadSharesCommand = new LoadSharesCommand(this, shareService);
            CreateShareCommand = new CreateShareCommand(this);
            DeleteShareCommand = new DeleteShareCommand(this, shareService, share =>
            {
                LoadSharesCommand.Execute(null);
                if (share?.Id == CurrentShare?.Id)
                {
                    LoadItemsCommand.Execute(null);
                }
            });
            AccessShareByIdCommand = new AccessShareByIdCommand(this);
            ListBoxSharesContextMenu = new ListBoxSharesContextMenu(this);
        }

        public MessageViewModel MessageViewModel { get; }
        public DataInputViewModel DataInputViewModel { get; }

        public Share CurrentShare
        {
            get => _currentShare;
            set
            {
                _currentShare = value;
                OnPropertyChanged(nameof(CurrentShare));
            }
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
        public ICommand CreateShareCommand { get; }
        public ICommand DeleteShareCommand { get; }
        public ICommand AccessShareByIdCommand { get; }

        public ICommand LoadItemsCommand { get; }

        public ContextMenu ListBoxSharesContextMenu { get; }
    }
}