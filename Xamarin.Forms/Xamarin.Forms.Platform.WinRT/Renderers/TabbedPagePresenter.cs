using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace Xamarin.Forms.Platform.WinRT.Renderers
{
    public class TabbedPagePresenter : ContentPresenter
    {
        public TabbedPagePresenter()
        {
        }


        //public override void OnApplyTemplate()
        //{
        //    TabbedPagePresenter.u003cu003ec__DisplayClass1 variable = null;
        //    base.OnApplyTemplate();
        //    DependencyObject parent = VisualTreeHelper.GetParent(this);
        //    while (parent != null && !(parent is PivotItem))
        //    {
        //        parent = VisualTreeHelper.GetParent(parent);
        //    }
        //    PivotItem pivotItem = parent as PivotItem;
        //    if (pivotItem == null)
        //    {
        //        throw new Exception("No parent PivotItem found for tab");
        //    }
        //    pivotItem.add_SizeChanged(new SizeChangedEventHandler(variable, (object s, SizeChangedEventArgs e) =>
        //    {
        //        if (this.pivotItem.get_ActualWidth() > 0 && this.pivotItem.get_ActualHeight() > 0)
        //        {
        //            ((TabbedPage)((Page)this.u003cu003e4__this.get_DataContext()).Parent).ContainerArea = new Rectangle(0, 0, this.pivotItem.get_ActualWidth(), this.pivotItem.get_ActualHeight());
        //        }
        //    }));
        //}
    }
}