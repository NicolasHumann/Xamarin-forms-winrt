using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Xamarin.Forms.Platform.WinRT.Renderers;

namespace Xamarin.Forms.Platform.WinRT.Controls
{
    public class CellTemplateSelector : DataTemplateSelector
    {
        protected override Windows.UI.Xaml.DataTemplate SelectTemplateCore(object item, DependencyObject container)
        {
            var cell = item as Cell;
            if (cell == null)
                return null;
            var handler = Registrar.Registered.GetHandler<ICellRenderer>(cell.GetType());
            return handler.GetTemplate(cell);
        }
    }
}