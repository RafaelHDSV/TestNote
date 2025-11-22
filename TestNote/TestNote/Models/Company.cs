using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestNote.Models
{
    [Table("Companies")]
    public class Company
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [Indexed]
        public string Name { get; set; } = string.Empty;
        public string Owner { get; set; } = string.Empty;
        public string Sector { get; set; } = string.Empty;
        public int NumberOfEmployees { get; set; }

        [Ignore]
        public string DisplayName => $"{Id} - {Name}";
    }
}
