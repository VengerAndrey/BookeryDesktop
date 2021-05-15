using System.Windows.Controls;
using System.Windows.Input;
using Domain.Models;

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
            _loadItemsCommand = loadItemsCommand;
            InitializeComponent();
        }

        public Item Item { get; set; }

        private void Grid_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            _loadItemsCommand.Execute(this);
        }
    }
}