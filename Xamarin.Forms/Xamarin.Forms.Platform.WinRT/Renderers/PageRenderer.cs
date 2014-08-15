using System;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Xamarin.Forms.Platform.WinRT;
using Xamarin.Forms.Platform.WinRT.Renderers;


namespace Xamarin.Forms.Platform.WinRT.Renderers
{
    //public class PageRenderer : VisualElementRenderer<Page, Panel>
    //{
      
    //    protected override void OnElementChanged(ElementChangedEventArgs<Page> e)
    //    {
    //        if (Element.BackgroundImage != null)
    //        {
    //            var imageBrush = new ImageBrush
    //                                    {
    //                                        ImageSource = new BitmapImage(new Uri(Element.BackgroundImage, UriKind.Relative))
    //                                    };
    //            SetValue(BackgroundProperty, imageBrush);
    //        }
    //        else if (Element.BackgroundColor != Color.Default)
    //        {
    //            SetValue(BackgroundProperty, Element.BackgroundColor.ToBrush());
    //        }

    //        base.OnElementChanged(e);
         
    //    }
    //}
}