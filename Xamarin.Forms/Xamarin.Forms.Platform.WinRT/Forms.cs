using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Windows.UI.Xaml;

namespace Xamarin.Forms.Platform.WinRT
{
    public static class Forms
    {
        internal static bool IsInitialized;

        public static void Init()
        {
            if (IsInitialized)
                return;

            Device.PlatformServices = new PlatformServices();

            Registrar.RegisterAll(new[]
                                  {
                                      typeof (ExportRendererAttribute),
                                      typeof (ExportImageSourceHandlerAttribute),
                                      typeof (ExportCellAttribute)
                                  });
            Device.OS = TargetPlatform.Other;
            Device.Idiom = TargetIdiom.Tablet;

            string name = typeof(Forms).GetType().Name;
            var myResourceDictionary = new Windows.UI.Xaml.ResourceDictionary
                                       {
                                           Source =
                                               new Uri("ms-appx:///Xamarin.Forms.Platform.WP8/WinRTResources.xaml",
                                               UriKind.RelativeOrAbsolute)
                                       };

            Application.Current.Resources.MergedDictionaries.Add(myResourceDictionary);
            //  ExpressionSearch.Default = new Forms.WinPhoneExpressionSearch();

            IsInitialized = true;
        }

        public static UIElement ConvertPageToUIElement(this Page page, Windows.UI.Xaml.Controls.Page applicationPage)
        {
            Platform platform = new Platform(applicationPage);
            platform.SetPage(page);
            return platform;
        }


        
    }
}

