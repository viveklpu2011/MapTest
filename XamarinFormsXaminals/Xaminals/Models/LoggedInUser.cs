using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Xaminals.Models
{
    public class LoggedInUser
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
        public DateTime Dob { get; set; }
    }
}
