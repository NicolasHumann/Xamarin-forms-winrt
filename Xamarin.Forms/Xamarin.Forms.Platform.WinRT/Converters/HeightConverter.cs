using System;

namespace Xamarin.Forms.Platform.WinRT.Converters
{
    public class HeightConverter : Windows.UI.Xaml.Data.IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string culture)
        {
            double num = (double)value;
            return (num > 0 ? num : double.NaN);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string culture)
        {
            throw new NotImplementedException();
        }
    }
}