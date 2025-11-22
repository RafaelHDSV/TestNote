using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace TestNote.Models
{
    [Table("Managers")]
    public class Manager
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        [Indexed(Name = "Idx_Manager_UserId", Unique = false)]
        public int UserId { get; set; }

        [Indexed(Name = "Idx_Manager_CompanyId", Unique = false)]
        public int CompanyId { get; set; }

        [Ignore]
        public string DisplayName => $"{Name} (Gerente)";
    }
}
