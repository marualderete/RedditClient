using System;
using System.IO;

using Xamarin.Forms;

using MyRedditApp.Droid.PlatformServices;
using MyRedditApp.PlatformServices;
using SQLite;

[assembly: Dependency(typeof(AppConnectionService))]
namespace MyRedditApp.Droid.PlatformServices
{
    public class AppConnectionService : IAppConnectionService
    {

        #region Private methods
        /// <summary>
        /// Returns the path folder where Db is stored on device
        /// </summary>
        /// <returns>The path.</returns>
        string GetPath()
        {
            var dbName = "RedditClientDB.db3";
            var path = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), dbName);
            return path;
        }

        #endregion

        #region Public methods and IAppConnectionService implementation

        /// <summary>
        /// Returns the file path to the db in device.
        /// </summary>
        /// <returns>The local file path.</returns>
        /// <param name="filename">Filename.</param>
        public string GetLocalFilePath(string filename)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            return Path.Combine(path, filename);
        }

        /// <summary>
        /// Gets the connection.
        /// </summary>
        /// <returns>The connection.</returns>
        public SQLiteConnection GetConnection()
        {
            return new SQLiteConnection(GetPath());
        }

		#endregion

    }
}
