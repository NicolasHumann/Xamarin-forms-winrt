using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Xamarin.Forms.Platform.WinRT;
using Xamarin.Forms.Platform.WinRT.Controls;
using Xamarin.Forms.Platform.WinRT.Renderers;

[assembly: ExportRenderer(typeof(Xamarin.Forms.ListView), typeof(ListViewRenderer))]
namespace Xamarin.Forms.Platform.WinRT.Renderers
{
    public class ListViewRenderer : ViewRenderer<ListView, ListBox>
    {
      

        protected override void OnElementChanged(ElementChangedEventArgs<ListView> e)
        {
            Element.ItemSelected += Element_ItemSelected;
        
            var listBox = new ListBox();
            listBox.DataContext = Element;
            listBox.ItemsSource = Element.TemplatedItems;

            listBox.ItemTemplateSelector = new CellTemplateSelector();
            listBox.Tapped += listBox_Tapped;
            listBox.Holding += listBox_Holding;

            SetNativeControl(listBox);
        }

        void Element_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            
        }

        void listBox_Holding(object sender, HoldingRoutedEventArgs e)
        {
           
        }

        void listBox_Tapped(object sender, TappedRoutedEventArgs e)
        {
          
        }

    }
}