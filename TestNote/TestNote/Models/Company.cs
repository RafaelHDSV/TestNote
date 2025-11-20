using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestNote.Models
{
    public class Company
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Owner { get; set; } = string.Empty;
        public string Sector { get; set; } = string.Empty;
        public int NumberOfEmployees { get; set; }


        public string DisplayName => $"{Id} - {Name}";
    }
}
