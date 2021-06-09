using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Domain.Models;
using WPF.Common;
using WPF.ViewModels;

namespace WPF.Controls
{
    /// <summary>
    ///     Interaction logic for ItemControl.xaml
    /// </summary>
    public partial class ItemControl : UserControl
    {
        private readonly ICommand _loadItemsCommand;

        public ItemControl(Item item, ICommand loadItemsCommand)
        {
            Item = item;
            Image = ItemImageHelper.GetImage(item);
            _loadItemsCommand = loadItemsCommand;
            InitializeComponent();
        }

        public Item Item { get; set; }
        public BitmapImage Image { get; set; }

        private void ItemControl_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _loadItemsCommand.Execute(Item);
        }
    }
}