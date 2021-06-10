using System.Windows;
using System.Windows.Controls;
using WPF.Controls;
using WPF.ViewModels;

namespace WPF.Common.ContextMenus
{
    internal class FileContextMenu : ContextMenu
    {
        public FileContextMenu(ItemsViewModel itemsViewModel, ItemControl caller)
        {
            var download = new MenuItem
            {
                Header = "Download",
                Command = itemsViewModel.DownloadFileCommand,
                CommandParameter = caller.Item,
                Icon = new Image
                {
                    Source = ContextMenuItemIconHelper.GetImage(ContextMenuIconName.Download)
                },
                Style = Application.Current.FindResource("StyleContextMenuItem") as Style
            };
            var delete = new MenuItem
            {
                Header = "Delete",
                Command = itemsViewModel.DeleteItemCommand,
                CommandParameter = caller.Item,
                Icon = new Image
                {
                    Source = ContextMenuItemIconHelper.GetImage(ContextMenuIconName.Delete)
                },
                Style = Application.Current.FindResource("StyleContextMenuItem") as Style
            };

            Items.Add(download);
            Items.Add(delete);

            Style = Application.Current.FindResource("StyleContextMenu") as Style;
        }
    }
}