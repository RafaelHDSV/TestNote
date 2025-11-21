using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestNote.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public int CompanyId { get; set; }
        // 1 = Admin, 2 = Gerente, 3 = Funcionário
        public int AccessLevel { get; set; }

        // Propriedade para exibição simples
        public string DisplayName => $"{Id} - {Name}";

        // Simulação de Níveis de Acesso
        public string RoleName => AccessLevel switch
        {
            1 => "Admin",
            2 => "Gerente",
            3 => "Funcionário",
            _ => "Desconhecido"
        };
    }
}
