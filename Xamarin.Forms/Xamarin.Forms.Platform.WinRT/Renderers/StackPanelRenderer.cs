using System.ComponentModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.WinRT;
using Xamarin.Forms.Platform.WinRT.Renderers;

//[assembly: ExportRenderer(typeof(StackLayout), typeof(StackPanelRenderer))]
// TODO: Use a specific renderer or let Xamarin Forms compute the position with the canvas ?
namespace Xamarin.Forms.Platform.WinRT.Renderers
{
    public class StackPanelRenderer : StackPanel, IWinRTRenderer
    {
        private VisualElement _element;

        public VisualElement Element
        {
            get { return _element; }
            set
            {
                _element = value;
                SetCurrentElement(value);
            }
        }

        private void SetCurrentElement(VisualElement element)
        {
            StackLayout layout = element as StackLayout;

            this.Orientation = layout.Orientation == StackOrientation.Horizontal
                ? Orientation.Horizontal
                : Orientation.Vertical;

            element.ChildAdded += element_ChildAdded;
            
            foreach (Element logicalElement in element.LogicalChildren)
            {
                element_ChildAdded(null, new ElementEventArgs(logicalElement));
            }
        }

        private void element_ChildAdded(object sender, ElementEventArgs e)
        {
            var visualElement = (VisualElement)e.Element;
            IWinRTRenderer renderer = RendererFactory.GetRenderer(visualElement);
            visualElement.SetRenderer(renderer);
            this.Children.Add((UIElement)renderer);
        }


        public SizeRequest GetDesiredSize(double widthConstraint, double heightConstraint)
        {
            return new SizeRequest(new Size(widthConstraint, heightConstraint));
        }
    }
}