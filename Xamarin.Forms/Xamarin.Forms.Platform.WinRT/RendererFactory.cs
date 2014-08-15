using Xamarin.Forms.Platform.WinRT.Renderers;


namespace Xamarin.Forms.Platform.WinRT
{
    public static class RendererFactory
    {
        public static IWinRTRenderer GetRenderer(VisualElement view)
        {

            IWinRTRenderer renderer = Registrar.Registered.GetHandler<IWinRTRenderer>(view.GetType()) ?? new ViewRenderer();
            renderer.Element = view;
            return renderer;
        }
    }
}