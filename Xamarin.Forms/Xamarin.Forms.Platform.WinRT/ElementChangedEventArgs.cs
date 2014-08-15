using System;
using System.Linq;

namespace Xamarin.Forms.Platform.WinRT
{
    public class ElementChangedEventArgs<TElement> : EventArgs where TElement : Element
    {
        public ElementChangedEventArgs(TElement oldElement, TElement newElement)
        {
            this.OldElement = oldElement;
            this.NewElement = newElement;
        }

        public TElement NewElement { get; private set; }

        public TElement OldElement { get; private set; }
    }
}

