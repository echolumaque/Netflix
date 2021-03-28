using System;
using System.IO;

namespace Netflix.Helpers.Database
{
    public class SQLiteConstants
    {
        public const string DatabaseFilename = "MyList.db3";

        public const SQLite.SQLiteOpenFlags Flags = SQLite.SQLiteOpenFlags.ReadWrite | SQLite.SQLiteOpenFlags.Create | SQLite.SQLiteOpenFlags.SharedCache;

        public static string DatabasePath
        {
            get
            {
                var databasePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                return Path.Combine(databasePath, DatabaseFilename);
            }
        }
    }
}
