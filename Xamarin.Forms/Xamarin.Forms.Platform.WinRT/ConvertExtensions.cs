using System.Collections.Generic;
using System.Linq;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Media;

namespace Xamarin.Forms.Platform.WinRT
{
    static class ConvertExtensions
    {
        public static Brush ToBrush(this Color color)
        {
            return new SolidColorBrush(color.ToMediaColor());
        }

        public static Windows.UI.Color ToMediaColor(this Color color)
        {
            return Windows.UI.Color.FromArgb((byte)(color.A * 255), (byte)(color.R * 255), (byte)(color.G * 255), (byte)(color.B * 255));
        }

        public static Windows.UI.Xaml.Thickness ToXamlThickness(this Thickness thickness)
        {
            return new Windows.UI.Xaml.Thickness(thickness.Left, thickness.Top, thickness.Right, thickness.Bottom);
        }

        public static Stretch ToStretch(this Aspect aspect)
        {
            switch (aspect)
            {
                case Aspect.AspectFit:
                    return Stretch.Uniform;
                case Aspect.AspectFill:
                    return Stretch.UniformToFill;
                case Aspect.Fill:
                    return Stretch.Fill;
                default:
                    return Stretch.Uniform;
            }
        }

        public static IEnumerable<Inline> ToInlines(this FormattedString formattedString)
        {
            return formattedString.Spans.Select(span => span.ToRun());
        }

        public static Run ToRun(this Span span)
        {
            if (string.IsNullOrEmpty(span.Text))
                return new Run();
            Run run = new Run { Text = span.Text };
            if (span.ForegroundColor != Color.Default)
                run.Foreground = span.ForegroundColor.ToBrush();
            return run;
        }
    }
}