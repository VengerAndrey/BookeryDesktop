using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Domain.Models;

namespace WPF.Common
{
    class ItemImageHelper
    {
        private static PathBuilder _pathBuilder = new PathBuilder();
        public static BitmapImage GetImage(Item item)
        {
            var bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            if (item.IsDirectory)
            {
                bitmapImage.UriSource = new Uri("../Resources/folder.png", UriKind.Relative);
            }
            else
            {
                var fileExtension = item.Path.Substring(item.Path.LastIndexOf(".") + 1);
                bitmapImage.UriSource = new Uri($"../Resources/ItemImages/{fileExtension}.png", UriKind.Relative);
            }
            bitmapImage.EndInit();

            return bitmapImage;
        }
    }
}
