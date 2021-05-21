using System.Windows.Controls;
using System.Windows.Input;
using Domain.Models;
using WPF.ViewModels;

namespace WPF.Views
{
    /// <summary>
    ///     Interaction logic for SharesView.xaml
    /// </summary>
    public partial class HomeView : UserControl
    {
        public HomeView()
        {
            InitializeComponent();
        }

        private void ShareBorder_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var homeViewModel = DataContext as HomeViewModel;
            var border = sender as Border;
            var textBlock = border?.Child as TextBlock;
            var share = textBlock?.DataContext as Share;
            homeViewModel?.LoadItemsCommand.Execute($"{share.Id}/root");
        }

        /*private void ItemBorder_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var homeViewModel = DataContext as HomeViewModel;
            var border = sender as Border;
            var textBlock = border?.Child as TextBlock;
            var item = textBlock?.DataContext as Item;
            homeViewModel?.LoadItemsCommand.Execute(item);
        }*/
    }
}