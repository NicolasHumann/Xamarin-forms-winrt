using System;
using System.Threading.Tasks;

namespace Xamarin.Forms.Platform.WinRT
{
    class IsolatedStorageFile : Xamarin.Forms.IIsolatedStorageFile
    {



        public void CreateDirectory(string path)
        {
            var t = Task.Run(() => Windows.Storage.ApplicationData.Current.LocalFolder.CreateFolderAsync(path));
            t.Wait();

        }

        public bool DirectoryExists(string path)
        {
            var t = Task.Run(async () =>
                             {
                                 try
                                 {
                                     await Windows.Storage.ApplicationData.Current.LocalFolder.GetFolderAsync(path);
                                     return true;
                                 }
                                 catch (Exception)
                                 {
                                     return false;
                                 }
                             }
                );
            t.Wait();
            return t.Result;
        }

        public bool FileExists(string path)
        {
            var t = Task.Run(async () =>
            {
                try
                {
                    await Windows.Storage.ApplicationData.Current.LocalFolder.GetFileAsync(path);
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
                 );
            t.Wait();
            return t.Result;
        }

        public DateTimeOffset GetLastWriteTime(string path)
        {
            var t = Task.Run(async () =>
            {
                try
                {
                    var f = await Windows.Storage.ApplicationData.Current.LocalFolder.GetFileAsync(path);
                    return f.DateCreated;
                }
                catch (Exception)
                {
                    return DateTimeOffset.MinValue;
                }
            });
            t.Wait();
            return t.Result;
        }

        public System.IO.Stream OpenFile(string path, FileMode mode, FileAccess access, FileShare share)
        {
           // return file.OpenFile(path, (System.IO.FileMode)mode, (System.IO.FileAccess)access, (System.IO.FileShare)share);
            return null;
        }

        public System.IO.Stream OpenFile(string path, FileMode mode, FileAccess access)
        {
            return null;
            //return file.OpenFile(path, (System.IO.FileMode)mode, (System.IO.FileAccess)access);
        }
    }
}
