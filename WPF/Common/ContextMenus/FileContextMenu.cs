using System.Windows;
using System.Windows.Controls;
using WPF.Controls;
using WPF.ViewModels;

namespace WPF.Common.ContextMenus
{
    class FileContextMenu : ContextMenu
    {
        public FileContextMenu(HomeViewModel homeViewModel, ItemControl caller)
        {
            var downloadFile = new MenuItem
            {
                Header = "Download",
                Command = homeViewModel.DownloadFileCommand,
                CommandParameter = caller,
                Icon = new Image
                {
                    Source = ContextMenuItemIconHelper.GetImage(ContextMenuIconName.Download)
                },
                Style = Application.Current.FindResource("StyleContextMenuItem") as Style
            };
            var delete = new MenuItem
            {
                Header = "Delete",
                Command = homeViewModel.DeleteItemCommand,
                CommandParameter = caller,
                Icon = new Image
                {
                    Source = ContextMenuItemIconHelper.GetImage(ContextMenuIconName.Delete)
                },
                Style = Application.Current.FindResource("StyleContextMenuItem") as Style
            };

            Items.Add(downloadFile);
            Items.Add(delete);

            Style = Application.Current.FindResource("StyleContextMenu") as Style;
        }
    }
}
