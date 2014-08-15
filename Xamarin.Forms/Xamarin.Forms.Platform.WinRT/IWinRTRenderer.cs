namespace Xamarin.Forms.Platform.WinRT
{
    public interface IWinRTRenderer : IRegisterable
    {
        VisualElement Element { get; set; }
        SizeRequest GetDesiredSize(double widthConstraint, double heightConstraint);
    }
}