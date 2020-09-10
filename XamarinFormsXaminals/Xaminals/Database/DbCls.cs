using System;
using SQLite;
using Xaminals.Models;

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
                database.CreateTable<LoggedInUser>();
            }
            catch (Exception ex)
            {

            }
        }
        public LoggedInUser GetUser()
        {
            return database.Table<LoggedInUser>().FirstOrDefault();
        }
        public LoggedInUser CheckLoggedInUser(string email)
        {
            return database.Table<LoggedInUser>().Where(x => x.Email == email).FirstOrDefault();
        }
        public LoggedInUser GetLoggedInUser(string email,string password)
        {
            return database.Table<LoggedInUser>().Where(x => x.Email == email && x.Password == password).FirstOrDefault();
        }

        public int SaveLoggedInUser(LoggedInUser item)
        {
            return database.Insert(item);
        }
    }
}
