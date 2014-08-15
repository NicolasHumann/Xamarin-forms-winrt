using System;
using System.Threading.Tasks;

namespace Xamarin.Forms.Platform.WinRT
{
    internal class NavigationService : INavigation
    {
        private static NavigationService _current;

        public NavigationService()
        {
            NavigationModel = new NavigationModel();
        }

        public NavigationModel NavigationModel { get; set; }

        public static NavigationService Current
        {
            get { return _current ?? (_current = new NavigationService()); }
        }

        public Task PushAsync(Page page)
        {
            this.Push(page, NavigationModel.CurrentPage);
            return Task.FromResult<Page>(page);
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
    }

    internal class PageEventArgs
    {
        public Page Page { get; set; }
    }
}