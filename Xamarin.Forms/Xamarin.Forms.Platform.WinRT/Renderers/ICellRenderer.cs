namespace Xamarin.Forms.Platform.WinRT.Renderers
{
    public interface ICellRenderer : IRegisterable
    {
        Windows.UI.Xaml.DataTemplate GetTemplate(Cell cell);
    }
}