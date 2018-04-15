using System;
using SQLite;

namespace SmokeT.Helpers
{
    public interface ISQLite
    {
        SQLiteConnection GetConnection();
    }
}
