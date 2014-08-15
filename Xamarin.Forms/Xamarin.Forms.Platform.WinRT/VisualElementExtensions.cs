namespace Xamarin.Forms.Platform.WinRT
{
    public static class VisualElementExtensions
    {
        public static IWinRTRenderer GetRenderer(this VisualElement self)
        {
            return (IWinRTRenderer)self.GetValue(Platform.RendererProperty);
        }

        public static void SetRenderer(this VisualElement self, IWinRTRenderer renderer)
        {
            self.SetValue(Platform.RendererProperty, renderer);
            self.IsPlatformEnabled = true;
        }
    }
}