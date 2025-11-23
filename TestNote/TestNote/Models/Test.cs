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
            get
            {
                if (string.IsNullOrEmpty(TestItemsString))
                    return new List<string>(); // Retorna lista vazia se nulo

                return TestItemsString.Split('|', StringSplitOptions.RemoveEmptyEntries).ToList();
            }
            set => TestItemsString = string.Join("|", value);
        }

        public int CompletedItemCount { get; set; }
        public string CompletedItemsString { get; set; } = string.Empty;
        public string ExecutionNotes { get; set; } = string.Empty;
        [Ignore]
        public int CompletionPercentage
        {
            get
            {
                var totalItems = TestItems.Count;
                if (totalItems == 0) return 0;
                return (int)(((double)CompletedItemCount / totalItems) * 100);
            }
        }

        [Ignore]
        public Color ProgressColor
        {
            get
            {
                if (Status == "Concluído" || CompletionPercentage == 100) return Color.FromArgb("#34C759"); // Verde
                if (Status == "Em Andamento") return Color.FromArgb("#FF9500"); // Laranja
                return Color.FromArgb("#555555"); // Cinza (Pendente)
            }
        }
    }
}
