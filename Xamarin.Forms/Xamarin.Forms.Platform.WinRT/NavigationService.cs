using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Xamarin.Forms.Platform.WinRT
{
    internal class NavigationService : INavigation, ICommand
    {
        private static NavigationService _current;

        public NavigationService()
        {
            NavigationModel = new NavigationModel();
            _current = this;
        }

        public NavigationModel NavigationModel { get; set; }

        public static NavigationService Current
        {
            get { return _current ?? (_current = new NavigationService()); }
        }

        public Task PushAsync(Page page)
        {
            this.Push(page, NavigationModel.CurrentPage);
            return Task.FromResult(page);
        }

        public Task<Page> PopAsync()
        {
            throw new NotImplementedException();
        }

        public Task PopToRootAsync()
        {
            throw new NotImplementedException();
        }

        public Task PushModalAsync(Page page)
        {
            throw new NotImplementedException();
        }

        public Task<Page> PopModalAsync()
        {
            throw new NotImplementedException();
        }

        public void Push(Page page, Page oldPage)
        {
            NavigationModel.Push(page, oldPage);

            if (OnNewPage != null)
                OnNewPage(this, new PageEventArgs { Page = this.NavigationModel.CurrentPage });
        }


        public event EventHandler<PageEventArgs> OnNewPage;
        public bool CanExecute(object parameter)
        {
            Page page = this.NavigationModel.Roots.Last();
            return page.Descendants().Count(p => p is Page) > 1;
            
        }

        public void Execute(object parameter)
        {
            Page currentPage = this.NavigationModel.CurrentPage;
            Page page = this.NavigationModel.Roots.Last();

            NavigationPage parent = currentPage.Parent as NavigationPage;
            if (parent != null)
            {
                parent.PopAsync();
                return;
            }
            if (this.NavigationModel.PopTopPage() != null)
            {
                if (OnNewPage != null)
                    OnNewPage(this, new PageEventArgs { Page = this.NavigationModel.CurrentPage });
            }
        }

        public event EventHandler CanExecuteChanged;

        public void Pop(Page oldPage)
        {
            Page page = this.NavigationModel.Pop(oldPage);
            if (OnNewPage != null)
                OnNewPage(this, new PageEventArgs { Page = this.NavigationModel.CurrentPage, UsePreviousPage = true });
        }
    }

    internal class PageEventArgs
    {
        public Page Page { get; set; }
        public bool UsePreviousPage { get; set; }
    }
}