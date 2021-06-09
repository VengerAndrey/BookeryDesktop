using System.Windows;
using System.Windows.Controls;
using WPF.ViewModels;

namespace WPF.Common.ContextMenus
{
    internal class ListBoxItemsContextMenu : ContextMenu
    {
        public ListBoxItemsContextMenu(ItemsViewModel itemsViewModel)
        {
            var createDirectory = new MenuItem
            {
                Header = "Create directory",
                Command = itemsViewModel.CreateDirectoryCommand,
                CommandParameter = itemsViewModel.CurrentItem,
                Icon = new Image
                {
                    Source = ContextMenuItemIconHelper.GetImage(ContextMenuIconName.CreateDirectory)
                },
                Style = Application.Current.FindResource("StyleContextMenuItem") as Style
            };
            var uploadFile = new MenuItem
            {
                Header = "Upload",
                Command = itemsViewModel.UploadFileCommand,
                CommandParameter = itemsViewModel.CurrentItem,
                Icon = new Image
                {
                    Source = ContextMenuItemIconHelper.GetImage(ContextMenuIconName.Upload)
                },
                Style = Application.Current.FindResource("StyleContextMenuItem") as Style
            };

            Items.Add(createDirectory);
            Items.Add(uploadFile);

            Style = Application.Current.FindResource("StyleContextMenu") as Style;
        }
    }
}