using System;
using SQLite;

namespace MyRedditApp.PlatformServices
{
    public interface IAppConnectionService
    {
        string GetLocalFilePath(string filename);

        SQLiteConnection GetConnection();

    }
}
