
using System;
using System.ComponentModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;
using Xamarin.Forms.Platform.WinRT;
using Xamarin.Forms.Platform.WinRT.Renderers;

[assembly: ExportRenderer(typeof(Xamarin.Forms.Button), typeof(ButtonRenderer))]
namespace Xamarin.Forms.Platform.WinRT.Renderers
{
    public class ButtonRenderer : VisualElementRenderer<Button, Windows.UI.Xaml.Controls.Button>
    {


        protected override void OnElementChanged(ElementChangedEventArgs<Button> e)
        {
            var button = new Windows.UI.Xaml.Controls.Button();
            button.Click += button_Click;
            SetNativeControl(button);

            UpdateContent();
        }

        void button_Click(object sender, RoutedEventArgs e)
        {
            if (Element != null)
            {
                Element.SendClicked();
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == Button.TextProperty.PropertyName || e.PropertyName == Button.ImageProperty.PropertyName)
            {
                UpdateContent();
                return;
            }
            base.OnElementPropertyChanged(sender, e);
        }

        private void UpdateContent()
        {
            if (Element.Image == null)
            {
                Control.Content = Element.Text;
                return;
            }
            Windows.UI.Xaml.Controls.Button control = Control;
            var stackPanel = new StackPanel { Orientation = Orientation.Vertical };

            var image = new Windows.UI.Xaml.Controls.Image();
            image.Source = new BitmapImage(new Uri(string.Concat("/", Element.Image.File), UriKind.Relative));
            image.Width = 30;
            image.Height = 30;
            image.Margin = new Windows.UI.Xaml.Thickness(0, 0, 20, 0);
            stackPanel.Children.Add(image);

            var textBlock = new TextBlock();
            textBlock.Text = Element.Text;
            stackPanel.Children.Add(textBlock);
            control.Content = stackPanel;
        }
    }
}