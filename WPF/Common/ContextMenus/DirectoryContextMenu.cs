﻿using System.Windows;
using System.Windows.Controls;
using WPF.Controls;
using WPF.ViewModels;

namespace WPF.Common.ContextMenus
{
    internal class DirectoryContextMenu : ContextMenu
    {
        public DirectoryContextMenu(ItemsViewModel itemsViewModel, ItemControl caller)
        {
            var deleteDirectory = new MenuItem
            {
                Header = "Delete",
                Command = itemsViewModel.DeleteItemCommand,
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