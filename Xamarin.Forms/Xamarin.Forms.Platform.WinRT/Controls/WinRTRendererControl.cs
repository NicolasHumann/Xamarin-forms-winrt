using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Xamarin.Forms.Platform.WinRT.Controls
{
    public class WinRTRendererControl : ContentControl
    {
        private readonly View _view;

        public WinRTRendererControl(View view)
        {
            _view = view;
            var renderer = RendererFactory.GetRenderer(view);
            view.SetRenderer(renderer);
            base.Content = renderer;
            var uiElement = renderer as FrameworkElement;
            if (uiElement != null)
                uiElement.Loaded += (s, e) => InvalidateMeasure();
        }

        protected override Windows.Foundation.Size ArrangeOverride(Windows.Foundation.Size finalSize)
        {
            _view.Layout(new Rectangle(0, 0, finalSize.Width, finalSize.Height));
            FrameworkElement content = Content as FrameworkElement;
            content.Arrange(new Rect(0, 0, finalSize.Width, finalSize.Height));
            return finalSize;
        }

        protected override Windows.Foundation.Size MeasureOverride(Windows.Foundation.Size availableSize)
        {
            ((FrameworkElement)Content).Measure(availableSize);
            SizeRequest sizeRequest = _view.GetSizeRequest(availableSize.Width, availableSize.Height);
            return new Windows.Foundation.Size(sizeRequest.Request.Width, sizeRequest.Request.Height);
        }
    }
}