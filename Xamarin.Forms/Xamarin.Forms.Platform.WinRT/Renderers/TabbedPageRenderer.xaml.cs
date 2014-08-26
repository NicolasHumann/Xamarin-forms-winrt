using System.Collections;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Xamarin.Forms.Platform.WinRT;
using Xamarin.Forms.Platform.WinRT.Renderers;


[assembly: ExportRenderer(typeof(Xamarin.Forms.TabbedPage), typeof(TabbedPageRenderer))]
namespace Xamarin.Forms.Platform.WinRT.Renderers
{
    public sealed partial class TabbedPageRenderer : UserControl, IWinRTRenderer
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

        public TabbedPageRenderer()
        {
            this.InitializeComponent();
        }


        public SizeRequest GetDesiredSize(double widthConstraint, double heightConstraint)
        {
            return new SizeRequest(new Size(widthConstraint, heightConstraint));
        }

        public void SetCurrentElement(VisualElement element)
        {
            DataContext = element;


            element.BatchCommitted += (ss,ee)=>
                                      {
                                          this.Height = ee.Data.Height;
                                          this.Width = ee.Data.Width;
                                      };
            this.Loaded += (ss, ee) => ((TabbedPage)element).SendAppearing();
            this.Unloaded += (ss, ee) => ((TabbedPage)element).SendDisappearing();

        }
    }
}
