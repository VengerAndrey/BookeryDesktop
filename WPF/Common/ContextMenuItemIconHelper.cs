using System;
using System.Windows.Media.Imaging;

namespace WPF.Common
{
    internal class ContextMenuItemIconHelper
    {
        public static BitmapImage GetImage(ContextMenuIconName iconName)
        {
            var bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            var fileName = iconName.ToString().ToLower();
            bitmapImage.UriSource = new Uri($"../Resources/ContextMenuIcons/{fileName}.png", UriKind.Relative);
            bitmapImage.EndInit();

            return bitmapImage;
        }
    }

    internal enum ContextMenuIconName
    {
        Download
    }
}