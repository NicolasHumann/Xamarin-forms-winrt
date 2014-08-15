using System.ComponentModel;
using Windows.UI.Xaml;
using Xamarin.Forms.Platform.WinRT;
using Xamarin.Forms.Platform.WinRT.Renderers;

[assembly: ExportRenderer(typeof(Xamarin.Forms.Slider), typeof(SliderRenderer))]
namespace Xamarin.Forms.Platform.WinRT.Renderers
{
    public class SliderRenderer : ViewRenderer<Slider, Windows.UI.Xaml.Controls.Slider>
    {

        protected override void OnElementChanged(ElementChangedEventArgs<Slider> e)
        {
            base.OnElementChanged(e);
            var slider = new Windows.UI.Xaml.Controls.Slider { Minimum = Element.Minimum, Maximum = Element.Maximum, Value = Element.Value };

            slider.ValueChanged += slider_ValueChanged;
            SetNativeControl(slider);
        }

        void slider_ValueChanged(object sender, Windows.UI.Xaml.Controls.Primitives.RangeBaseValueChangedEventArgs e)
        {
            Element.Value = e.NewValue;
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (e.PropertyName == Slider.MinimumProperty.PropertyName)
            {
                Control.Minimum = Element.Minimum;
                return;
            }
            if (e.PropertyName == Slider.MaximumProperty.PropertyName)
            {
                Control.Maximum = Element.Maximum;
                return;
            }
            if (e.PropertyName == Slider.ValueProperty.PropertyName)
            {
                Control.Value = Element.Value;
            }
        }
    }
}