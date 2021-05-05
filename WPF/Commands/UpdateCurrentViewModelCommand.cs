using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WPF.State.Navigators;
using WPF.ViewModels;

namespace WPF.Commands
{
    class UpdateCurrentViewModelCommand : ICommand
    {
        private readonly INavigator _navigator;

        public event EventHandler? CanExecuteChanged;

        public UpdateCurrentViewModelCommand(INavigator navigator)
        {
            _navigator = navigator;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (parameter is ViewType viewType)
            {
                switch (viewType)
                {
                    case ViewType.Containers:
                        _navigator.CurrentViewModel = new ContainersViewModel();
                        break;
                    case ViewType.Mock:
                        _navigator.CurrentViewModel = new MockViewModel();
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

    }
}
