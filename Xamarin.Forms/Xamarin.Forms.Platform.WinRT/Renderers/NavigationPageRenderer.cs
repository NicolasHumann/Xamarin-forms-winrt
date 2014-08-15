using System;
using System.Linq;
using Windows.UI.Xaml;
using Xamarin.Forms;
using Xamarin.Forms.Platform.WinRT;
using Xamarin.Forms.Platform.WinRT.Renderers;


[assembly: ExportRenderer(typeof(NavigationPage), typeof(NavigationPageRenderer))]

namespace Xamarin.Forms.Platform.WinRT.Renderers
{
    public class NavigationPageRenderer : VisualElementRenderer<NavigationPage, FrameworkElement>
    {
        public NavigationPageRenderer()
        {
            AutoPackage = false;
        }

        protected override void OnElementChanged(ElementChangedEventArgs<NavigationPage> e)
        {
            base.OnElementChanged(e);

            Element.Pushed += Element_Pushed;
            //Element.Popped += this.PageOnPopped;
            //Element.PoppedToRoot += this.PageOnPoppedToRoot;

            Page[] array = base.Element.StackCopy.Reverse().ToArray();
            if (array.Any())
            {
                Page page = array.First();
                page.IgnoresContainerArea = true;
                var renderer = RendererFactory.GetRenderer(page);
                page.SetRenderer(renderer);
                base.Children.Add(renderer as UIElement);
            }
        }

        void Element_Pushed(object sender, NavigationEventArgs e)
        {
            NavigationService.Current.Push(e.Page, Element);
        }
    }
}