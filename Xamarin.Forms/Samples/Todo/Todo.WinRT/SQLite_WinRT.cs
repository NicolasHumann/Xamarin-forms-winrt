using System.IO;
using Windows.Storage;
using Todo.WinRT;
using Xamarin.Forms;

[assembly: Dependency(typeof(SQLite_WinRT))]
namespace Todo.WinRT
{
    public class SQLite_WinRT : ISQLite
    {
        public SQLite_WinRT()
        {
        }

        #region ISQLite implementation
        public SQLite.Net.SQLiteConnection GetConnection()
        {
            var sqliteFilename = "TodoSQLite.db3";
            string path = Path.Combine(ApplicationData.Current.LocalFolder.Path, sqliteFilename);

            var plat = new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT();
            var conn = new SQLite.Net.SQLiteConnection(plat, path);

            // Return the database connection 
            return conn;
        }
        #endregion
    }
}