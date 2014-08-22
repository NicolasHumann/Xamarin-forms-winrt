using System.ComponentModel;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;

namespace Xamarin.Forms.Platform.WinRT.Renderers
{
    public class ViewRenderer<TElement, TNativeElement> : VisualElementRenderer<TElement, TNativeElement>
        where TElement : View
        where TNativeElement : FrameworkElement
    {
        protected override void OnElementChanged(ElementChangedEventArgs<TElement> e)
        {
            base.OnElementChanged(e);

           
        }
    }

    public class ViewRenderer : ViewRenderer<View, FrameworkElement>
    {
        protected override void OnElementChanged(ElementChangedEventArgs<View> e)
        {
            base.OnElementChanged(e);
            base.SizeChanged += (s, ee) => UpdateClipToBounds();
         
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (e.PropertyName == Layout.IsClippedToBoundsProperty.PropertyName)
            {
                UpdateClipToBounds();
            }
        }

        private void UpdateClipToBounds()
        {
            Layout element = Element as Layout;
            if (element != null)
            {
                Clip = null;
                if (element.IsClippedToBounds)
                {
                    RectangleGeometry rectangleGeometry = new RectangleGeometry();
                    rectangleGeometry.Rect = new Rect(0, 0, base.ActualWidth, base.ActualHeight);
                    Clip = rectangleGeometry;
                }
            }
        }
    }
}