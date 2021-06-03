using System.Windows.Input;
using WPF.Commands;

namespace WPF.ViewModels
{
    public class DataInputViewModel : BaseViewModel
    {
        private string _name;

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

        private string _value;

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

        public DataInputViewModel()
        {
            CancelCommand = new CancelDataInputCommand(this);
        }
    }
}
