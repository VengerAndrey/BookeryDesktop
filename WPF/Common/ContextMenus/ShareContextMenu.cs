using System.Windows;
using System.Windows.Controls;
using Domain.Models;
using WPF.ViewModels;

namespace WPF.Common.ContextMenus
{
    internal class ShareContextMenu : ContextMenu
    {
        public ShareContextMenu(SharesViewModel sharesViewModel, Share caller)
        {
            var deleteDirectory = new MenuItem
            {
                Header = "Delete",
                Command = sharesViewModel.DeleteShareCommand,
                CommandParameter = caller,
                Icon = new Image
                {
                    Source = ContextMenuItemIconHelper.GetImage(ContextMenuIconName.Delete)
                },
                Style = Application.Current.FindResource("StyleContextMenuItem") as Style
            };

            Items.Add(deleteDirectory);

            Style = Application.Current.FindResource("StyleContextMenu") as Style;
        }
    }
}