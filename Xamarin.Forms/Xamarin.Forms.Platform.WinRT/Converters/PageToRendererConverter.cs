using System;
using System.Globalization;

namespace Xamarin.Forms.Platform.WinRT.Converters
{
    public class PageToRendererConverter : Windows.UI.Xaml.Data.IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            Page page = value as Page;
            if (page == null)
                return null;

            IWinRTRenderer renderer = RendererFactory.GetRenderer(page);
            page.SetRenderer(renderer);
            return renderer;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}