using System;
using System.Linq;

namespace Xamarin.Forms.Platform.WinRT
{
    public class ElementChangedEventArgs<TElement> : EventArgs where TElement : Element
    {
        public ElementChangedEventArgs(TElement oldElement, TElement newElement)
        {
           OldElement = oldElement;
           NewElement = newElement;
        }

        public TElement NewElement { get; set; }
        public TElement OldElement { get; set; }
    }
}

