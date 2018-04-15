using System;
using System.IO;
using SmokeT.Droid.Data;
using SmokeT.Helpers;
using Xamarin.Forms;

[assembly: Dependency(typeof(SQLite_Android))]
namespace SmokeT.Droid.Data
{
    public class SQLite_Android : ISQLite
    {
        public SQLite_Android()
        {
        }

        public SQLite.SQLiteConnection GetConnection(){
            var sqliteFileName = "SmokeT.bd3";
            string documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            var path = Path.Combine(documentsPath,sqliteFileName);
            var conn = new SQLite.SQLiteConnection(path);

            return conn;
        }
    }
}
