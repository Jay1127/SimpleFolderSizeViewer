using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Interop;
using System.Windows.Media.Imaging;

namespace SimpleFolderSizeViewer.App
{
    class IconToImageSourceConverter : IValueConverter
    {
        public object Convert(object icon, Type targetType, object parameter, CultureInfo culture)
        {
            if (icon == null)
            {
                return DependencyProperty.UnsetValue;
            }

            var sysicon = icon as Icon;
            return Imaging.CreateBitmapSourceFromHIcon(sysicon.Handle, 
                                                       Int32Rect.Empty, 
                                                       BitmapSizeOptions.FromEmptyOptions());
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DependencyProperty.UnsetValue;
        }
    }
}
