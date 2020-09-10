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
                database.CreateTable<LoggedIn>();
            }
            catch (Exception ex)
            {

            }
        }
        public LoggedIn GetUser()
        {
            return database.Table<LoggedIn>().FirstOrDefault();
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
        public int SaveLoggedIn(LoggedIn item)
        {
            return database.Insert(item);
        }
        public int ClearLoginDetails()
        {
            var status = 0;
            try
            {
                var data = database.Table<LoggedIn>().ToList();
                foreach (var item in data)
                {
                    status = database.Delete(item);
                }

            }
            catch (Exception ex)
            {

            }
            return status;
        }

    }
}
