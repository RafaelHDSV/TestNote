using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestNote.Models
{
    [Table("Employees")]
    public class Employee
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;
        public string JobTitle { get; set; } = string.Empty;

        [Indexed(Name = "Idx_Employee_UserId", Unique = false)]
        public int UserId { get; set; }

        [Indexed(Name = "Idx_Employee_CompanyId", Unique = false)]
        public int CompanyId { get; set; }

        [Ignore]
        public string Email { get; set; } = string.Empty; // Preenchido via Join lógico

        [Ignore]
        public int AccessLevel { get; set; } = 3; // Padrão funcionário

        [Ignore]
        public string DisplayName => $"{Id} - {Name}";

        [Ignore]
        public string RoleName => "Funcionário";
    }
}
