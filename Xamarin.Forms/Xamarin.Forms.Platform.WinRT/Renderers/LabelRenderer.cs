using System.ComponentModel;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Documents;
using Xamarin.Forms.Platform.WinRT;
using Xamarin.Forms.Platform.WinRT.Renderers;

[assembly: ExportRenderer(typeof(Xamarin.Forms.Label), typeof(LabelRenderer))]
namespace Xamarin.Forms.Platform.WinRT.Renderers
{

    public class LabelRenderer : ViewRenderer<Label, TextBlock>
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
        {
            SetNativeControl(new TextBlock());

            UpdateText();
            UpdateAlign();
        }

        private void UpdateText()
        {
            if (Element.FormattedText == null)
                Element.FormattedText = Element.Text;

            Control.Inlines.Clear();
            foreach (Inline inline in Element.FormattedText.ToInlines())
                Control.Inlines.Add(inline);
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == Label.TextProperty.PropertyName)
            {
                UpdateText();
            }
            else if (e.PropertyName == Label.TextColorProperty.PropertyName)
            {
                // TODO
            }
            else if (e.PropertyName == Label.XAlignProperty.PropertyName)
            {
                UpdateAlign();
            }
            else if (e.PropertyName == Label.FontProperty.PropertyName)
            {
                // TODO
            }
            else if (e.PropertyName == Label.LineBreakModeProperty.PropertyName)
            {
                // TODO
            }
            base.OnElementPropertyChanged(sender, e);
        }

        private void UpdateAlign()
        {
            if (Element.XAlign == TextAlignment.Start)
            {
                Control.TextAlignment = Windows.UI.Xaml.TextAlignment.Center;
                return;
            }
            if (Element.XAlign == TextAlignment.End)
            {
                Control.TextAlignment = Windows.UI.Xaml.TextAlignment.Left;
                return;
            }
            Control.TextAlignment = Windows.UI.Xaml.TextAlignment.Center;
        }
    }
}