using Windows.UI.Xaml;
using Xamarin.Forms;
using Xamarin.Forms.Platform.WinRT;
using Xamarin.Forms.Platform.WinRT.Renderers;

[assembly: ExportCell(typeof(ViewCell), typeof(ViewCellRenderer))]
namespace Xamarin.Forms.Platform.WinRT.Renderers
{
    public class ViewCellRenderer : ICellRenderer
    {
        public virtual Windows.UI.Xaml.DataTemplate GetTemplate(Cell cell)
        {
            return (Windows.UI.Xaml.DataTemplate)Application.Current.Resources["ViewCell"];
        }
    }
}