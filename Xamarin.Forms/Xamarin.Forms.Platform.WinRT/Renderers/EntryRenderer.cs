using System;
using System.ComponentModel;
using System.Linq;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Xamarin.Forms.Platform.WinRT;
using Xamarin.Forms.Platform.WinRT.Renderers;

[assembly: ExportRenderer(typeof(Xamarin.Forms.Entry), typeof(EntryRenderer))]
namespace Xamarin.Forms.Platform.WinRT.Renderers
{
    public class EntryRenderer : ViewRenderer<Entry, Canvas>
    {
        private TextBox _textBox;
        private PasswordBox _passwordBox;
        private bool _passwordBoxHasFocus;

        public override SizeRequest GetDesiredSize(double widthConstraint, double heightConstraint)
        {
            if (!Children.Any())
                return new SizeRequest();
            var size = new Windows.Foundation.Size(widthConstraint, heightConstraint);
            var item = (FrameworkElement)Control.Children.First();
            double width = item.Width;
            double height = item.Height;
            item.Height = double.NaN;
            item.Width = double.NaN;
            item.Measure(size);

            var newDesiredSize = new Size(Math.Ceiling(item.DesiredSize.Width), Math.Ceiling(item.DesiredSize.Height));
            item.Width = width;
            item.Height = height;
            return new SizeRequest(newDesiredSize);
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);
            _textBox = new TextBox();
            _textBox.LostFocus += OnTextBoxUnfocused;
            _passwordBox = new PasswordBox();
            var canvas = new Canvas();
            canvas.Children.Add(_textBox);
            canvas.Children.Add(_passwordBox);
            canvas.SizeChanged += (sender, args) =>
            {
                _passwordBox.Height = canvas.Height;
                _textBox.Height = canvas.Height;

                _passwordBox.Width = canvas.Width;
                _textBox.Width = canvas.Width;
            };
            SetNativeControl(canvas);
            UpdateText();

            UpdateColor();
            _textBox.TextChanged += TextBoxOnTextChanged;
            _textBox.KeyUp += TextBoxOnKeyUp;
            _passwordBox.PasswordChanged += PasswordBoxOnPasswordChanged;
            _passwordBox.KeyUp += TextBoxOnKeyUp;
            _passwordBox.GotFocus += (sender, args) =>
            {
                _passwordBoxHasFocus = true;
                UpdateControl();
            };
            _passwordBox.LostFocus += (sender, args) =>
            {
                _passwordBoxHasFocus = false;
                UpdateControl();
                if (Element.TextColor != Color.Default && !string.IsNullOrEmpty(Element.Text))
                {
                    _passwordBox.Foreground = Element.TextColor.ToBrush();
                }
            };
            _textBox.IsEnabled = Element.IsEnabled;
            _passwordBox.IsEnabled = Element.IsEnabled;
            UpdateControl();
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (e.PropertyName == Entry.TextProperty.PropertyName)
            {
                UpdateControl();
                UpdateText();
                return;
            }
            if (e.PropertyName == Entry.PlaceholderProperty.PropertyName)
            {
                // TODO
                return;
            }
            if (e.PropertyName == Entry.IsPasswordProperty.PropertyName)
            {
                UpdateControl();
                return;
            }
            if (e.PropertyName == VisualElement.IsEnabledProperty.PropertyName)
            {
                _textBox.IsEnabled = Element.IsEnabled;
                _passwordBox.IsEnabled = Element.IsEnabled;
                return;
            }
            if (e.PropertyName == Entry.TextColorProperty.PropertyName)
            {
                UpdateColor();
                return;
            }
            if (e.PropertyName == InputView.KeyboardProperty.PropertyName)
            {
                // TODO
            }
        }



        private void OnTextBoxUnfocused(object sender, RoutedEventArgs e)
        {
            if (Element.TextColor == Color.Default)
            {
                return;
            }
            if (!string.IsNullOrEmpty(Element.Text))
            {
                _textBox.Foreground = Element.TextColor.ToBrush();
            }
        }

        private void PasswordBoxOnPasswordChanged(object sender, RoutedEventArgs routedEventArgs)
        {
            Element.Text = _passwordBox.Password;
        }

        private void TextBoxOnKeyUp(object sender, KeyRoutedEventArgs keyRoutedEventArgs)
        {
            if (keyRoutedEventArgs.Key == VirtualKey.Cancel)
                Element.SendCompleted();
        }

        private void TextBoxOnTextChanged(object sender, Windows.UI.Xaml.Controls.TextChangedEventArgs textChangedEventArgs)
        {
            Element.Text = _textBox.Text;
        }

        private void UpdateColor()
        {
            if (_textBox == null)
                return;

            if (Element == null)
            {
                _textBox.Foreground = (Brush)Windows.UI.Xaml.Controls.Control.ForegroundProperty.GetMetadata(typeof(TextBox)).DefaultValue;
                _passwordBox.Foreground = (Brush)Windows.UI.Xaml.Controls.Control.ForegroundProperty.GetMetadata(typeof(PasswordBox)).DefaultValue;
            }
            else if (!string.IsNullOrEmpty(Element.Text))
            {
                if (Element.TextColor != Color.Default)
                {
                    _textBox.Foreground = Element.TextColor.ToBrush();
                    _passwordBox.Foreground = _textBox.Foreground;
                    return;
                }
                _textBox.Foreground = (Brush)Windows.UI.Xaml.Controls.Control.ForegroundProperty.GetMetadata(typeof(TextBox)).DefaultValue;
                _passwordBox.Foreground = (Brush)Windows.UI.Xaml.Controls.Control.ForegroundProperty.GetMetadata(typeof(PasswordBox)).DefaultValue;
            }
        }

        private void UpdateControl()
        {
            if (!Element.IsPassword)
            {
                _passwordBox.Opacity = 0;
                _textBox.Opacity = 1;
                _passwordBox.IsHitTestVisible = false;
                _textBox.IsHitTestVisible = true;
                return;
            }
            if (!string.IsNullOrEmpty(Element.Text) || _passwordBoxHasFocus)
            {
                _passwordBox.Opacity = 1;
                _textBox.Opacity = 0;
            }
            else
            {
                _passwordBox.Opacity = 0;
                _textBox.Opacity = 1;
            }
            _passwordBox.IsHitTestVisible = true;
            _textBox.IsHitTestVisible = false;
        }

        private void UpdateText()
        {
            _textBox.Text = Element.Text ?? string.Empty;
            _textBox.Select(_textBox.Text.Length, 0);
            _passwordBox.Password = Element.Text ?? string.Empty;
        }
    }
}