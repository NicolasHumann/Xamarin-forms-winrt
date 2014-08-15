
using System;
using System.ComponentModel;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Xamarin.Forms;
using Xamarin.Forms.Platform.WinRT;
using Xamarin.Forms.Platform.WinRT.Renderers;

[assembly: ExportImageSourceHandler(typeof(FileImageSource), typeof(FileImageSourceHandler))]
[assembly: ExportImageSourceHandler(typeof(StreamImageSource), typeof(StreamImagesourceHandler))]
[assembly: ExportImageSourceHandler(typeof(UriImageSource), typeof(ImageLoaderSourceHandler))]
[assembly: ExportRenderer(typeof(Xamarin.Forms.Image), typeof(ImageRenderer))]
namespace Xamarin.Forms.Platform.WinRT.Renderers
{
    public class ImageRenderer : ViewRenderer<Image, Windows.UI.Xaml.Controls.Image>
    {
        public override SizeRequest GetDesiredSize(double widthConstraint, double heightConstraint)
        {
            if (Control.Source == null)
                return new SizeRequest();
            return new SizeRequest(new Size
                        {
                            Width = ((BitmapImage)Control.Source).PixelWidth,
                            Height = ((BitmapImage)Control.Source).PixelHeight
                        });
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Image> e)
        {
            base.OnElementChanged(e);
            var image = new Windows.UI.Xaml.Controls.Image();
            image.Stretch = Element.Aspect.ToStretch();

            SetImageSource(image);
            
            SetNativeControl(image);
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (e.PropertyName == Image.SourceProperty.PropertyName)
            {
                SetImageSource(Control);
                return;
            }
            if (e.PropertyName == Image.AspectProperty.PropertyName)
            {
                Control.Stretch = Element.Aspect.ToStretch();
            }
        }

        private async void SetImageSource(Windows.UI.Xaml.Controls.Image image)
        {
            if (Element.Source != null)
            {
                var handler = Registrar.Registered.GetHandler<IImageSourceHandler>(Element.Source.GetType());
                if (handler == null)
                {
                    image.Source = null;
                    return;
                }
                Windows.UI.Xaml.Media.ImageSource imageSource;
                try
                {
                    imageSource = await handler.LoadImageAsync(Element.Source, CancellationToken.None);
                }
                catch (Exception ex)
                {
                    imageSource = null;
                }
                image.Source = imageSource;
                Element.NativeSizeChanged();
                return;
            }
            image.Source = null;
        }
    }


    public interface IImageSourceHandler : IRegisterable
    {
        Task<Windows.UI.Xaml.Media.ImageSource> LoadImageAsync(ImageSource imagesoure, CancellationToken cancelationToken);
    }

    public sealed class StreamImagesourceHandler : IImageSourceHandler
    {
        public async Task<Windows.UI.Xaml.Media.ImageSource> LoadImageAsync(ImageSource imagesource, CancellationToken cancelationToken)
        {
            BitmapImage bitmapImage = null;
            StreamImageSource streamImageSource = imagesource as StreamImageSource;
            if (streamImageSource != null && streamImageSource.Stream != null)
            {
                using (Stream stream = await streamImageSource.GetStreamAsync(cancelationToken))
                {
                    IRandomAccessStream inMemoryStream = new InMemoryRandomAccessStream();
                    using (var inputStream = stream.AsInputStream())
                    {
                        await RandomAccessStream.CopyAsync(inputStream, inMemoryStream);
                    }
                    inMemoryStream.Seek(0);

                    bitmapImage = new BitmapImage();
                    bitmapImage.SetSource(inMemoryStream);
                }
            }
            return bitmapImage;
        }
    }

    public sealed class ImageLoaderSourceHandler : IImageSourceHandler
    {
        public  Task<Windows.UI.Xaml.Media.ImageSource> LoadImageAsync(ImageSource imagesoure, CancellationToken cancelationToken)
        {
            //BitmapImage bitmapImage = null;
            //UriImageSource uriImageSource = imagesoure as UriImageSource;
            //if (uriImageSource != null && uriImageSource.Uri != null)
            //{
            //    using (Stream stream = await uriImageSource.GetStreamAsync(cancelationToken))
            //    {
            //        if (stream != null && stream.CanRead)
            //        {
            //            IRandomAccessStream inMemoryStream = new InMemoryRandomAccessStream();
            //            using (var inputStream = stream.AsInputStream())
            //            {
            //                await RandomAccessStream.CopyAsync(inputStream, inMemoryStream);
            //            }
            //            inMemoryStream.Seek(0);

            //            bitmapImage = new BitmapImage();
            //            bitmapImage.SetSource(inMemoryStream);
            //        }
            //    }
            //}
            //return bitmapImage;
            Windows.UI.Xaml.Media.ImageSource bitmapImage = null;
            UriImageSource fileImageSource = imagesoure as UriImageSource;
            if (fileImageSource != null)
            {

                bitmapImage = new BitmapImage(fileImageSource.Uri);
            }
            return Task.FromResult(bitmapImage);
        }
    }

    public sealed class FileImageSourceHandler : IImageSourceHandler
    {
        public Task<Windows.UI.Xaml.Media.ImageSource> LoadImageAsync(ImageSource imagesoure, CancellationToken cancelationToken)
        {
            Windows.UI.Xaml.Media.ImageSource bitmapImage = null;
            FileImageSource fileImageSource = imagesoure as FileImageSource;
            if (fileImageSource != null)
            {
                string file = fileImageSource.File;
                bitmapImage = new BitmapImage(new Uri(string.Concat("/", file), UriKind.Relative));
            }
            return Task.FromResult(bitmapImage);
        }
    }
}