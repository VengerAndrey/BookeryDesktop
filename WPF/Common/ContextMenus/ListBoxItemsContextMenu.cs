using System.Windows;
using System.Windows.Controls;
using WPF.ViewModels;

namespace WPF.Common.ContextMenus
{
    internal class ListBoxItemsContextMenu : ContextMenu
    {
        public ListBoxItemsContextMenu(HomeViewModel homeViewModel)
        {
            var createDirectory = new MenuItem
            {
                Header = "Create directory",
                Command = homeViewModel.CreateDirectoryCommand,
                CommandParameter = homeViewModel.CurrentItem,
                Icon = new Image
                {
                    Source = ContextMenuItemIconHelper.GetImage(ContextMenuIconName.CreateDirectory)
                },
                Style = Application.Current.FindResource("StyleContextMenuItem") as Style
            };
            var uploadFile = new MenuItem
            {
                Header = "Upload",
                Command = homeViewModel.UploadFileCommand,
                CommandParameter = homeViewModel.CurrentItem,
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