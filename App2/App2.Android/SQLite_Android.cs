using System;
using App2.Droid;
using System.IO;
using Xamarin.Forms;

[assembly: Dependency(typeof(SQLite_Android))]
namespace App2.Droid
{
    public class SQLite_Android : ISQLite
    {
        public SQLite_Android() { }
        public string GetDatabasePath(string sqliteFilename)
        {
            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var path = Path.Combine(documentsPath, sqliteFilename);
            return path;
        }
    }
}