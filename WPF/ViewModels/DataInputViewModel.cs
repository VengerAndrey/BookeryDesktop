using System;
using System.Windows.Input;
using WPF.Commands;

namespace WPF.ViewModels
{
    public class DataInputViewModel : BaseViewModel
    {
        private string _name;

        private string _value;

        public DataInputViewModel()
        {
            CancelCommand = new CancelDataInputCommand(this);
        }

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
                OnPropertyChanged(nameof(IsShown));
            }
        }

        public string Value
        {
            get => _value;
            set
            {
                _value = value;
                OnPropertyChanged(nameof(Value));
            }
        }

        public bool IsShown => !string.IsNullOrEmpty(Name);

        public ICommand CancelCommand { get; }

        public DataInputType DataInputType { get; private set; }

        public void Show(DataInputType dataInputType)
        {
            DataInputType = dataInputType;
            switch (dataInputType)
            {
                case DataInputType.CreateShare:
                case DataInputType.RenameShare:
                    Name = "Enter course name:";
                    break;
                case DataInputType.CreateDirectory:
                    Name = "Enter directory name:";
                    break;
                case DataInputType.AccessShareById:
                    Name = "Enter course id:";
                    break;
                case DataInputType.RenameFile:
                    Name = "Enter file name:";
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(dataInputType), dataInputType, null);
            }
        }
    }

    public enum DataInputType
    {
        CreateShare,
        CreateDirectory,
        AccessShareById,
        RenameShare,
        RenameFile
    }
}