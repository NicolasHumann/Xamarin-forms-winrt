using System.ComponentModel;
using Windows.UI.Xaml.Controls;
using Xamarin.Forms.Platform.WinRT;
using Xamarin.Forms.Platform.WinRT.Renderers;

[assembly: ExportRenderer(typeof(Xamarin.Forms.Switch), typeof(SwitchRenderer))]
namespace Xamarin.Forms.Platform.WinRT.Renderers
{
    public class SwitchRenderer : ViewRenderer<Switch, ToggleSwitch>
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Switch> e)
        {
            base.OnElementChanged(e);

            var toggleSwitch = new ToggleSwitch();
            toggleSwitch.IsOn = e.NewElement.IsToggled;
            SetNativeControl(toggleSwitch);
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (e.PropertyName == Switch.IsToggledProperty.PropertyName)
            {
                Control.IsOn = Element.IsToggled;
            }
        }

    }
}