using System.Windows;
using System.Windows.Controls;
using WPF.ViewModels;

namespace WPF.Common.ContextMenus
{
    internal class ListBoxSharesContextMenu : ContextMenu
    {
        public ListBoxSharesContextMenu(SharesViewModel sharesViewModel)
        {
            var createShare = new MenuItem
            {
                Header = "Create",
                Command = sharesViewModel.CreateShareCommand,
                Icon = new Image
                {
                    Source = ContextMenuItemIconHelper.GetImage(ContextMenuIconName.CreateDirectory)
                },
                Style = Application.Current.FindResource("StyleContextMenuItem") as Style
            };
            var accessShareById = new MenuItem
            {
                Header = "Access by ID",
                Command = sharesViewModel.AccessShareByIdCommand,
                Icon = new Image
                {
                    Source = ContextMenuItemIconHelper.GetImage(ContextMenuIconName.CreateDirectory)
                },
                Style = Application.Current.FindResource("StyleContextMenuItem") as Style
            };

            Items.Add(createShare);
            Items.Add(accessShareById);

            Style = Application.Current.FindResource("StyleContextMenu") as Style;
        }
    }
}