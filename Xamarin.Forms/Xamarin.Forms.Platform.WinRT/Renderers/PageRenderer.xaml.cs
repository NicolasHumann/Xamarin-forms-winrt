
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234237
using Xamarin.Forms.Platform.WinRT;
using Xamarin.Forms.Platform.WinRT.Renderers;

[assembly: ExportRenderer(typeof(Xamarin.Forms.Page), typeof(PageRenderer))]
namespace Xamarin.Forms.Platform.WinRT.Renderers
{
    /// <summary>
    /// A basic page that provides characteristics common to most applications.
    /// </summary>
    public sealed partial class PageRenderer : PageRendererControl
    {
        public PageRenderer()
        {
            this.InitializeComponent();

            this.Loaded+=(s,e) => base.Element.SendAppearing();
            this.Unloaded+=(s,e) => base.Element.SendDisappearing();
        }

        protected override void SetChildren(UIElement element)
        {
            TheContentPresenter.Content = element;
        }

        protected override void UpdateNativeControl()
        {

            if (Element.Parent is NavigationPage)
                TitleGrid.Visibility = Visibility.Visible;
            else
                TitleGrid.Visibility = Visibility.Collapsed;

            base.UpdateNativeControl();
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Page> e)
        {
            DataContext = Element;

            Element.BatchCommitted += (ss, ee) =>
            {
                this.Height = ee.Data.Height;
                this.Width = ee.Data.Width;
            };

            if (Element.BackgroundImage != null)
            {
                this.Background = new ImageBrush
                 {
                     ImageSource = new BitmapImage(new Uri(Element.BackgroundImage, UriKind.Relative))
                 };
            }
            else if (Element.BackgroundColor != Color.Default)
            {
                this.Background = Element.BackgroundColor.ToBrush();
            }

            base.OnElementChanged(e);

        }
    }

    public class PageRendererControl : VisualElementRenderer<Page, Panel>
    {

    }
}
