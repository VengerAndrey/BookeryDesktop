using System.Timers;

namespace WPF.ViewModels
{
    public class MessageViewModel : BaseViewModel
    {
        private string _message;
        private readonly Timer _timer;

        public MessageViewModel()
        {
            _timer = new Timer(3000);
            _timer.AutoReset = false;
            _timer.Elapsed += (sender, args) =>
            {
                _message = "";
                OnPropertyChanged(nameof(Message));
                OnPropertyChanged(nameof(HasMessage));
            };
        }

        public string Message
        {
            get => _message;
            set
            {
                _message = value;
                OnPropertyChanged(nameof(Message));
                OnPropertyChanged(nameof(HasMessage));
                _timer.Start();
            }
        }

        public bool HasMessage => !string.IsNullOrEmpty(Message);
    }
}