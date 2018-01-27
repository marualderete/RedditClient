using System;

using Xamarin.Forms;

using MyRedditApp.iOS.PlatformServices;
using MyRedditApp.PlatformServices;
using System.IO;
using SQLite;

[assembly: Dependency(typeof(AppConnectionService))]
namespace MyRedditApp.iOS.PlatformServices
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
            string personalFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string libraryFolder = Path.Combine(personalFolder, "..", "Library");
            var path = Path.Combine(libraryFolder, dbName);

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
            string docFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string libFolder = Path.Combine(docFolder, "..", "Library", "Databases");

            if (!Directory.Exists(libFolder))
            {
                Directory.CreateDirectory(libFolder);
            }

            return Path.Combine(libFolder, filename);
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
