using System;
using System.Linq;
using System.Linq.Expressions;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Xamarin.Forms.Platform.WinRT
{
    public class Platform : BindableObject, IPlatform, IPlatformEngine
    {
        private Windows.UI.Xaml.Controls.Page _page;
        private readonly Canvas _renderer;
        private readonly ToolbarTracker _tracker;

        public IPlatformEngine Engine { get { return this; } }
        public Page Page { get; private set; }
        public bool Supports3D { get; private set; }

        public Platform(Windows.UI.Xaml.Controls.Page page)
        {
            _page = page;
            _renderer = new Canvas();
            _renderer.SizeChanged += renderer_SizeChanged;
            NavigationService.Current.OnNewPage += Current_OnNewPage;
            _tracker = new ToolbarTracker();
            _tracker.CollectionChanged += (s, e) => UpdateAppBar();

        }

        private void UpdateAppBar()
        {
            if (_page.BottomAppBar != null)
            {
                ((CommandBar)_page.BottomAppBar).PrimaryCommands.Clear();
            }
            var commandBar = new CommandBar();

            foreach (var toolbarItem in _tracker.ToolbarItems)
            {
                var button = new AppBarButton
                             {
                                 Label = toolbarItem.Name,
                                 Icon = new BitmapIcon { UriSource = new Uri(toolbarItem.Icon.File) }
                             };
                ToolbarItem item = toolbarItem;
                button.Click += (s, e) => item.Activate();
                commandBar.PrimaryCommands.Add(button);
            }



            _page.BottomAppBar = commandBar;
        }


        void Current_OnNewPage(object sender, PageEventArgs e)
        {
            SetCurrentPage(e.Page, e.UsePreviousPage);
        }

        public static implicit operator UIElement(Platform canvas)
        {
            return canvas._renderer;
        }

        private void renderer_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            NavigationService.Current.NavigationModel.Roots.ForEach(f => f.Layout(new Rectangle(0.0, 0.0, _renderer.ActualWidth, _renderer.ActualHeight)));
        }

        public void SetPage(Page page)
        {
            NavigationService.Current.NavigationModel.Push(page, null);
            SetCurrentPage(page);
            page.NavigationProxy.Inner = NavigationService.Current;

        }

        private void SetCurrentPage(Page page, bool usePreviousPage = false)
        {
            page.Platform = this;

            if (page.GetRenderer() == null)
            {
                Renderer = RendererFactory.GetRenderer(page);
                page.SetRenderer(Renderer);
            }

            page.Layout(new Rectangle(0, 0, _renderer.ActualWidth, _renderer.ActualHeight));
            if (usePreviousPage)
            {
                Renderer = page.GetRenderer();
                UpdateAppBar();
            }
            _renderer.Children.Clear();
            _renderer.Children.Add((UIElement)Renderer);

            if (NavigationService.Current.NavigationModel.Roots.Last() != null)
            {
                _tracker.Target = NavigationService.Current.NavigationModel.Roots.Last();
            }
        }


        public SizeRequest GetNativeSize(VisualElement view, double widthConstraint, double heightConstraint)
        {
            if (((widthConstraint > 0.0) && (heightConstraint > 0.0)) && (view.GetRenderer() != null))
            {
                return view.GetRenderer().GetDesiredSize(widthConstraint, heightConstraint);
            }
            return new SizeRequest();
        }

        public static readonly BindableProperty RendererProperty =
           BindableProperty.Create((Expression<Func<Platform, IWinRTRenderer>>)(w => w.Renderer), null);

        public Platform(Windows.UI.Xaml.Controls.Frame applicationFrame)
        {

        }

        private IWinRTRenderer Renderer
        {
            get { return (IWinRTRenderer)base.GetValue(RendererProperty); }
            set
            {
                base.SetValue(RendererProperty, value);
            }
        }
    }
}