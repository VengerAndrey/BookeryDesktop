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
            var copyShareId = new MenuItem
            {
                Header = "Copy ID",
                Command = sharesViewModel.CopyShareIdCommand,
                CommandParameter = caller,
                Icon = new Image
                {
                    Source = ContextMenuItemIconHelper.GetImage(ContextMenuIconName.Share)
                },
                Style = Application.Current.FindResource("StyleContextMenuItem") as Style
            };
            var renameShare = new MenuItem
            {
                Header = "Rename",
                Command = sharesViewModel.OpenDataInputCommand,
                CommandParameter = DataInputType.RenameShare,
                Icon = new Image
                {
                    Source = ContextMenuItemIconHelper.GetImage(ContextMenuIconName.Rename)
                },
                Style = Application.Current.FindResource("StyleContextMenuItem") as Style
            };
            var deleteShare = new MenuItem
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

            Items.Add(copyShareId);
            Items.Add(renameShare);
            Items.Add(deleteShare);

            Style = Application.Current.FindResource("StyleContextMenu") as Style;
        }
    }
}