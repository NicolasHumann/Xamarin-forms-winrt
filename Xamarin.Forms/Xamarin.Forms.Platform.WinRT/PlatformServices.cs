using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.UI.Core;

namespace Xamarin.Forms.Platform.WinRT
{
    class PlatformServices : IPlatformServices
    {
        readonly HttpClient _httpClient = new HttpClient();

        // Thread _uiThread;

        public bool IsInvokeRequired
        {
            get
            {
                lock (this)
                {
                    return CoreApplication.MainView.CoreWindow.Dispatcher.HasThreadAccess;
                }
            }
        }

        public async void BeginInvokeOnMainThread(Action action)
        {
            await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => action());
        }

        public ITimer CreateTimer(Action<object> callback, object state, int dueTime, int period)
        {
            return new ThreadTimer(callback, state, dueTime, period);
        }

        public ITimer CreateTimer(Action<object> callback)
        {
            return new ThreadTimer(callback);
        }

        public ITimer CreateTimer(Action<object> callback, object state, long dueTime, long period)
        {
            return new ThreadTimer(callback, state, dueTime, period);
        }

        public ITimer CreateTimer(Action<object> callback, object state, uint dueTime, uint period)
        {
            return new ThreadTimer(callback, state, dueTime, period);
        }

        public ITimer CreateTimer(Action<object> callback, object state, TimeSpan dueTime, TimeSpan period)
        {
            return new ThreadTimer(callback, state, dueTime, period);
        }

        public Assembly[] GetAssemblies()
        {
            var t = Task.Run(async () =>
            {
                List<Assembly> assemblies = new List<Assembly>();

                var files = await Windows.ApplicationModel.Package.Current.InstalledLocation.GetFilesAsync();

                foreach (var file in files)
                {
                    if ((file.FileType == ".dll") || (file.FileType == ".exe"))
                    {
                        try
                        {
                            AssemblyName name = new AssemblyName() { Name = Path.GetFileNameWithoutExtension(file.Name) };
                            Assembly asm = Assembly.Load(name);
                            assemblies.Add(asm);
                        }
                        catch (Exception e)
                        {
                            System.Diagnostics.Debug.WriteLine(e.Message);
                        }

                    }
                }

                return assemblies.ToArray();
            });
            t.Wait();
            return t.Result;

        }

        public async Task<Stream> GetStreamAsync(Uri uri, CancellationToken cancellationToken)
        {
            // Web file
            var response = await _httpClient.GetAsync(uri, cancellationToken);
            return await response.Content.ReadAsStreamAsync();
        }

        public IIsolatedStorageFile GetUserStoreForApplication()
        {
            return new IsolatedStorageFile();
        }

        public void OpenUriAction(Uri uri)
        {
        }

        public void StartTimer(TimeSpan interval, Func<bool> callback)
        {
            Timer timer = null;
            TimerCallback timerCallback = delegate
            {
                if (!callback())
                    timer.Dispose();
            };

            timer = new Timer(timerCallback, null, interval, interval);
        }
    }
}