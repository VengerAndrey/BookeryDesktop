using System;
using System.Windows.Media.Imaging;
using Domain.Models;

namespace WPF.Common
{
    internal class ItemImageHelper
    {
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
                if (Enum.TryParse(typeof(FileExtension), fileExtension, true, out var res))
                {
                    bitmapImage.UriSource = new Uri($"../Resources/ItemImages/{fileExtension}.png", UriKind.Relative);
                }
                else
                {
                    bitmapImage.UriSource = new Uri("../Resources/ItemImages/unknown.png", UriKind.Relative);
                }
            }

            bitmapImage.EndInit();

            return bitmapImage;
        }

        private enum FileExtension
        {
            Doc,
            Docx,
            Mp3,
            Pdf,
            Png,
            Ppt,
            Pptx,
            Txt,
            Xls,
            Xlsx,
            Zip
        }
    }
}