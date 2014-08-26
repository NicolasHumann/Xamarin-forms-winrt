using System;
using System.ComponentModel;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace Xamarin.Forms.Platform.WinRT.Renderers
{
    public class VisualElementRenderer<TElement, TNativeElement> : Windows.UI.Xaml.Controls.Canvas, IWinRTRenderer
        where TElement : VisualElement
        where TNativeElement : FrameworkElement
    {
        private VisualElement _element;
        private VisualElement _oldElement;
        private TNativeElement _child;

        public TElement Element
        {
            get { return (TElement)_element; }
        }

        public bool AutoPackage { get; set; }
        public TNativeElement Control { get; set; }

        public VisualElementRenderer()
        {
            AutoPackage = true;
        }

        VisualElement IWinRTRenderer.Element
        {
            get { return _element; }
            set
            {
                _oldElement = _element;
                _element = value;
                SetCurrentElement(value);
            }
        }

        private void SetCurrentElement(VisualElement element)
        {
            element.PropertyChanged += OnElementPropertyChanged;

            if (AutoPackage)
            {
                element.ChildAdded += element_ChildAdded;
                element.ChildRemoved += element_ChildRemoved;
                foreach (Element logicalElement in element.LogicalChildren)
                {
                    element_ChildAdded(null, new ElementEventArgs(logicalElement));
                }
            }
            element.BatchCommitted += Element_BatchCommitted;

            OnElementChanged(new ElementChangedEventArgs<TElement>((TElement)_oldElement, (TElement)element));
            UpdateNativeControl();
        }

        void element_ChildRemoved(object sender, ElementEventArgs e)
        {
            // TODO
        }

        void element_ChildAdded(object sender, ElementEventArgs e)
        {
            var visualElement = (VisualElement)e.Element;
            IWinRTRenderer renderer = RendererFactory.GetRenderer(visualElement);
            visualElement.SetRenderer(renderer);
            SetChildren((UIElement)renderer);
        }

        void Element_BatchCommitted(object sender, EventArg<VisualElement> e)
        {
            UpdateNativeControl();
        }

        protected virtual void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == VisualElement.BackgroundColorProperty.PropertyName)
            {
              // TODO
            }
        }

        protected virtual void OnElementChanged(ElementChangedEventArgs<TElement> e)
        {

        }

        protected virtual void SetChildren(UIElement element)
        {
            Children.Add(element);
        }

        protected virtual void SetNativeControl(TNativeElement element)
        {
            Control = element;
            SetChildren(element);

            _child = element;

            Element.IsNativeStateConsistent = false;
            element.Loaded += (s, e) => Element.IsNativeStateConsistent = true;
            element.GotFocus += (s, e) => Element.IsFocused = true;
            element.LostFocus += (s, e) => Element.IsFocused = false;

          
        }

        protected virtual void UpdateNativeControl()
        {
            if (Element.Batched || Element.Bounds.IsEmpty)
            {
                Opacity = 0;
                return;
            }
            Visibility = (Element.IsVisible ? Visibility.Visible : Visibility.Collapsed);
            if (Element.Width >= 0)
                Width = Element.Width;
            if (Element.Height >= 0)
                Height = Element.Height;

            if (_child != null)
            {
                _child.Width = Width;
                _child.Height = Height;
            }
            Canvas.SetLeft(this, Element.X + Element.TranslationX);
            Canvas.SetTop(this, Element.Y + Element.TranslationY);
            Opacity = Element.Opacity;

            RenderTransformOrigin = new Windows.Foundation.Point(Element.AnchorX, Element.AnchorY);

            RenderTransform = new ScaleTransform { ScaleX = Element.Scale, ScaleY = Element.Scale };

            Projection = new PlaneProjection
                                              {
                                                  CenterOfRotationX = Element.AnchorX,
                                                  CenterOfRotationY = Element.AnchorY,
                                                  RotationX = Element.RotationX,
                                                  RotationY = Element.RotationY,
                                                  RotationZ = -Element.Rotation
                                              };

            InvalidateArrange();

        }

        public virtual SizeRequest GetDesiredSize(double widthConstraint, double heightConstraint)
        {
            if (!Children.Any())
                return new SizeRequest();

            var size = new Windows.Foundation.Size(widthConstraint, heightConstraint);
            var element = (FrameworkElement)Children.First();
            double width = element.Width;
            double height = element.Height;
            element.Height = double.NaN;
            element.Width = double.NaN;
            element.Measure(size);

            var newDesiredSize = new Size(Math.Ceiling(element.DesiredSize.Width), Math.Ceiling(element.DesiredSize.Height));
            element.Width = width;
            element.Height = height;

            return new SizeRequest(newDesiredSize);
        }

    }
}