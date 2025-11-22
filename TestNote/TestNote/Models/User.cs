using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestNote.Models
{
    [Table("Users")]
    public class User
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [Indexed(Name = "Idx_User_Email", Unique = true)]
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

        // 1 = Admin, 2 = Manager, 3 = Employee
        public int Role { get; set; }
    }
}
