using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TabbedPageDemo
{
    public partial class TabbedPageDemoPage
    {
        public TabbedPageDemoPage()
        {
            InitializeComponent();

            this.ItemsSource = MonkeyDataModel.All;
        }
    }
}
