using System.Collections;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236
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

            //DataContext(element);
            //page.PropertyChanged += new PropertyChangedEventHandler(this.OnPropertyChanged);
            //add_Loaded(new RoutedEventHandler(this, (object sender, RoutedEventArgs args) => this.page.SendAppearing()));
            //add_Unloaded(new RoutedEventHandler(this, (object sender, RoutedEventArgs args) => this.page.SendDisappearing()));
            this.Loaded += (ss, ee) => ((TabbedPage)element).SendAppearing();
            this.Unloaded += (ss, ee) => ((TabbedPage)element).SendDisappearing();

        }
    }
}
