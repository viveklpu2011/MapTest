using System;
using SQLite;

namespace Xaminals.Database
{
    public class DbCls
    {
        readonly SQLiteConnection database;

        public DbCls(string dbPath)
        {
            try
            {
                database = new SQLiteConnection(dbPath);
            }
            catch (Exception ex)
            {

            }
        }
    }
}
