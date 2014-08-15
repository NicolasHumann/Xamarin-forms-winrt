using System;
using Xamarin.Forms.Platform.WinRT.Controls;

namespace Xamarin.Forms.Platform.WinRT.Converters
{
    public class ViewToRendererConverter : Windows.UI.Xaml.Data.IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string culture)
        {
            View view = value as View;
            if (view == null)
                return null;
            return new WinRTRendererControl(view);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string culture)
        {
            throw new NotSupportedException();
        }
    }
}