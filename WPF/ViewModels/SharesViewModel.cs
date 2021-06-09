using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Input;
using BookeryApi.Services.Storage;
using BookeryApi.Services.User;
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
            IShareService shareService, IAccessService accessService, ICommand loadItemsCommand,
            ICommand openDataInputCommand)
        {
            MessageViewModel = messageViewModel;
            DataInputViewModel = dataInputViewModel;

            LoadSharesCommand = loadItemsCommand;
            OpenDataInputCommand = openDataInputCommand;

            var callback = new Action(() =>
            {
                LoadSharesCommand.Execute(null);
                DataInputViewModel.CancelCommand.Execute(null);
            });

            LoadSharesCommand = new LoadSharesCommand(this, shareService);
            CreateShareCommand = new CreateShareCommand(shareService, callback);
            DeleteShareCommand = new DeleteShareCommand(this, shareService, share =>
            {
                LoadSharesCommand.Execute(null);
                if (share?.Id == CurrentShare?.Id)
                {
                    loadItemsCommand.Execute(null);
                }

                OnPropertyChanged(nameof(CurrentShare));
            });
            AccessShareByIdCommand = new AccessShareByIdCommand(accessService, this, callback);
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

        public ICommand OpenDataInputCommand { get; }

        public ContextMenu ListBoxSharesContextMenu { get; }
    }
}