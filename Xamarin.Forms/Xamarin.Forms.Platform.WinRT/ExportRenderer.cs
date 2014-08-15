using System;

namespace Xamarin.Forms.Platform.WinRT
{
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple=true)]
    public class ExportRendererAttribute : HandlerAttribute, IRegisterable
    {
        public Type ViewType;
        public Type RendererType;

        public ExportRendererAttribute(Type viewType, Type rendererType)
            : base(viewType, rendererType)
        {
            ViewType = viewType;
            RendererType = rendererType;
        }
    }

    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
    public sealed class ExportImageSourceHandlerAttribute : HandlerAttribute
    {
        public ExportImageSourceHandlerAttribute(Type handler, Type target)
            : base(handler, target)
        {
        }
    }

    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
    public sealed class ExportCellAttribute : HandlerAttribute
    {
        public ExportCellAttribute(Type handler, Type target)
            : base(handler, target)
        {
        }
    }
}
