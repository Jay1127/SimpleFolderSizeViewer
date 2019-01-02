using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace SimpleFolderSizeViewer.App
{
    class BoolToVisibilityConverter : IValueConverter
    {
        public object Convert(object boolean, Type targetType, object parameter, CultureInfo culture)
        {
            if(boolean is null || !(boolean is bool))
            {
                return DependencyProperty.UnsetValue;
            }

            return (bool)boolean ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object visibility, Type targetType, object parameter, CultureInfo culture)
        {
            return (Visibility)visibility == Visibility.Visible;
        }
    }
}
