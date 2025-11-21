using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestNote.Models
{
    public class Test
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Status { get; set; } = "Pendente"; // Ex: Pendente, Em Andamento, Concluído
        public int CompanyId { get; set; }

        // Simulação de dados do teste para exibição
        public List<string> TestItems => new List<string>
        {
            "1. Validar Fluxo de Login",
            "2. Testar Funcionalidade X",
            "3. Verificar Relatórios Y"
        };
    }
}
