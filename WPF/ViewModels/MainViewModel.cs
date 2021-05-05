using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF.State.Navigators;

namespace WPF.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public INavigator Navigator { get; set; } = new Navigator();
    }
}
