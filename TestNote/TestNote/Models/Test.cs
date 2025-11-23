using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestNote.Models
{
    [Table("Tests")]
    public class Test
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        
        // Status: "Pendente", "Em Andamento", "Concluído", "Aprovado", "Reprovado"
        public string Status { get; set; } = "Pendente";
        [Indexed(Name = "Idx_Test_CompanyId", Unique = false)]
        public int CompanyId { get; set; }

        public int CreatorId { get; set; }

        public int? ExecutorId { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? TestedAt { get; set; }

        public string TestItemsString { get; set; } = string.Empty;

        [Ignore]
        public List<string> TestItems
        {
            get => string.IsNullOrEmpty(TestItemsString) ? new List<string>() : TestItemsString.Split('|').ToList();
            set => TestItemsString = string.Join("|", value);
        }
    }
}
