using System.Windows;
using System.Windows.Controls;
using WPF.ViewModels;

namespace WPF.Common.ContextMenus
{
    internal class ProfilePhotoContextMenu : ContextMenu
    {
        public ProfilePhotoContextMenu(UserViewModel userViewModel)
        {
            var set = new MenuItem
            {
                Header = "Set new",
                Command = userViewModel.SetProfilePhotoCommand,
                CommandParameter = null,
                Icon = new Image
                {
                    Source = ContextMenuItemIconHelper.GetImage(ContextMenuIconName.Upload)
                },
                Style = Application.Current.FindResource("StyleContextMenuItem") as Style
            };

            Items.Add(set);

            Style = Application.Current.FindResource("StyleContextMenu") as Style;
        }
    }
}