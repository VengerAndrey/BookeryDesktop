using System.Windows;
using System.Windows.Controls;
using WPF.ViewModels;

namespace WPF.Common.ContextMenus
{
    internal class ListBoxSharesContextMenu : ContextMenu
    {
        public ListBoxSharesContextMenu(HomeViewModel homeViewModel)
        {
            var createShare = new MenuItem
            {
                Header = "Create course",
                Command = homeViewModel.CreateShareCommand,
                Icon = new Image
                {
                    Source = ContextMenuItemIconHelper.GetImage(ContextMenuIconName.CreateDirectory)
                },
                Style = Application.Current.FindResource("StyleContextMenuItem") as Style
            };

            Items.Add(createShare);

            Style = Application.Current.FindResource("StyleContextMenu") as Style;
        }
    }
}