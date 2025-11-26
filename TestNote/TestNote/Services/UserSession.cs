using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestNote.Models;

namespace TestNote.Services
{
    public static class UserSession
    {
        public static User? CurrentUser { get; set; }
        public static Employee? CurrentEmployeeProfile { get; set; }
    }
}
