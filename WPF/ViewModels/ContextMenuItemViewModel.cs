using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace WPF.ViewModels
{
    internal class ContextMenuItemViewModel
    {
        public string Header { get; set; }
        public ICommand Command { get; set; }
        public BitmapImage Image { get; set; }
    }
}