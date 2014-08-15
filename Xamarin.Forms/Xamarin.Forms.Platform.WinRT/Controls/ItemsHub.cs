using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Xamarin.Forms.Platform.WinRT.Controls
{
    /// <summary>
    /// A Hub Control with bindable ItemTemplate and ItemsSource.
    /// </summary>
    public class ItemsHub : Hub
    {

        public static readonly DependencyProperty ItemHeaderTemplateProperty =
            DependencyProperty.Register("ItemHeaderTemplate", typeof(Windows.UI.Xaml.DataTemplate), typeof(ItemsHub), new PropertyMetadata(default(DataTemplate)));

        public Windows.UI.Xaml.DataTemplate ItemHeaderTemplate
        {
            get { return (Windows.UI.Xaml.DataTemplate)GetValue(ItemHeaderTemplateProperty); }
            set { SetValue(ItemHeaderTemplateProperty, value); }
        }

        #region ItemTemplate Dependency Property

        public Windows.UI.Xaml.DataTemplate ItemTemplate
        {
            get { return (Windows.UI.Xaml.DataTemplate)GetValue(ItemTemplateProperty); }
            set { SetValue(ItemTemplateProperty, value); }
        }

        public static readonly DependencyProperty ItemTemplateProperty =
            DependencyProperty.Register("ItemTemplate", typeof(Windows.UI.Xaml.DataTemplate), typeof(ItemsHub), new PropertyMetadata(null, ItemTemplateChanged));

        private static void ItemTemplateChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ItemsHub hub = d as ItemsHub;
            if (hub != null)
            {
                Windows.UI.Xaml.DataTemplate template = e.NewValue as Windows.UI.Xaml.DataTemplate;
                if (template != null)
                {
                    // Apply template
                    foreach (var section in hub.Sections)
                    {
                        section.ContentTemplate = template;
                    }
                }
            }
        }

        #endregion

        #region ItemsSource Dependency Property

        public IEnumerable ItemsSource
        {
            get { return (IEnumerable)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        public static readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.Register("ItemsSource", typeof(IEnumerable), typeof(ItemsHub), new PropertyMetadata(null, ItemsSourceChanged));

        private static void ItemsSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ItemsHub hub = d as ItemsHub;
            if (hub != null)
            {
                IEnumerable items = e.NewValue as IEnumerable;
                if (items != null)
                {
                    // hub.Sections.Clear();



                    var sec = hub.Sections.Where(h => (string)h.Tag == "dyn").ToList();

                    foreach (var hubSection in sec)
                    {
                        hub.Sections.Remove(hubSection);
                    }


                    foreach (var item in items)
                    {
                        HubSection section = new HubSection();
                        
                        section.Tag = "dyn";
                        section.DataContext = item;
                        section.HeaderTemplate = hub.ItemHeaderTemplate;
                        Windows.UI.Xaml.DataTemplate template = hub.ItemTemplate;
                        section.ContentTemplate = template;
                        //hub.Sections.Add(section);
                        hub.Sections.Insert(0, section);
                    }
                }
            }
        }

        #endregion
    }
}