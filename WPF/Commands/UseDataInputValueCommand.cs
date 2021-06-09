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

        public UseDataInputValueCommand(DataInputViewModel dataInputViewModel, ICommand createShareCommand,
            ICommand createDirectoryCommand, ICommand accessShareByIdCommand)
        {
            _dataInputViewModel = dataInputViewModel;
            _createShareCommand = createShareCommand;
            _createDirectoryCommand = createDirectoryCommand;
            _accessShareByIdCommand = accessShareByIdCommand;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            switch (_dataInputViewModel.DataInputType)
            {
                case DataInputType.ShareName:
                    _createShareCommand.Execute(parameter);
                    break;
                case DataInputType.DirectoryName:
                    _createDirectoryCommand.Execute(parameter);
                    break;
                case DataInputType.ShareId:
                    _accessShareByIdCommand.Execute(parameter);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public event EventHandler CanExecuteChanged;
    }
}