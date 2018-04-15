using System;
using System.IO;
using Xamarin.Forms;

[assembly: Dependency(typeof(SmokeT.iOS.Data.SQLite_IOS))]
namespace SmokeT.iOS.Data
{
    public class SQLite_IOS
    {
        public SQLite_IOS()
        {
        }
        public SQLite.SQLiteConnection GetConnection()
        {
            var sqliteFileName = "SmokeT.bd3";
            string documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            var libraryPath = Path.Combine(documentsPath,"..", "Library");
            var path = Path.Combine(libraryPath, sqliteFileName);
            var connection = new SQLite.SQLiteConnection(path);

            return connection;
        }
    }
}
