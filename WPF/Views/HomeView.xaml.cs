using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Domain.Models;
using WPF.Common.ContextMenus;
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
            if (homeViewModel is null)
            {
                return;
            }

            var border = sender as Border;
            var contentPresenter = border?.Child as ContentPresenter;
            var share = contentPresenter?.Content as Share;
            var item = new Item
            {
                Name = "root",
                IsDirectory = true,
                Size = null,
                Path = $"{share?.Id}/root"
            };
            homeViewModel.CurrentItem = item;
            homeViewModel.CurrentShare = share;
            homeViewModel.LoadItemsCommand.Execute(item.Path);
        }

        private void ShareBorder_OnMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            var homeViewModel = DataContext as HomeViewModel;
            if (homeViewModel is null)
            {
                return;
            }

            var border = sender as Border;
            var contentPresenter = border?.Child as ContentPresenter;
            var share = contentPresenter?.Content as Share;

            border.ContextMenu = new ShareContextMenu(homeViewModel, share);
        }

        private void ListBoxItems_OnLoaded(object sender, RoutedEventArgs e)
        {
            var homeViewModel = DataContext as HomeViewModel;
            if (homeViewModel is null)
            {
                return;
            }

            homeViewModel.CurrentItemChanged += () =>
            {
                ListBoxItems.ContextMenu = homeViewModel.ListBoxItemsContextMenu;
            };
        }
    }
}