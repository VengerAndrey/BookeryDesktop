using System;
using System.Windows.Input;
using WPF.ViewModels;

namespace WPF.Commands
{
    internal class UseDataInputValueCommand : ICommand
    {
        private readonly ICommand _accessShareByIdCommand;
        private readonly ICommand _createDirectoryCommand;
        private readonly ICommand _createShareCommand;
        private readonly DataInputViewModel _dataInputViewModel;
        private readonly ICommand _renameShareCommand;

        public UseDataInputValueCommand(DataInputViewModel dataInputViewModel, ICommand createShareCommand,
            ICommand createDirectoryCommand, ICommand accessShareByIdCommand, ICommand renameShareCommand)
        {
            _dataInputViewModel = dataInputViewModel;
            _createShareCommand = createShareCommand;
            _createDirectoryCommand = createDirectoryCommand;
            _accessShareByIdCommand = accessShareByIdCommand;
            _renameShareCommand = renameShareCommand;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            switch (_dataInputViewModel.DataInputType)
            {
                case DataInputType.CreateShare:
                    _createShareCommand.Execute(parameter);
                    break;
                case DataInputType.CreateDirectory:
                    _createDirectoryCommand.Execute(parameter);
                    break;
                case DataInputType.AccessShareById:
                    _accessShareByIdCommand.Execute(parameter);
                    break;
                case DataInputType.RenameShare:
                    _renameShareCommand.Execute(parameter);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public event EventHandler CanExecuteChanged;
    }
}