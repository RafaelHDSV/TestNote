using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestNote.Models;
using TestNote.Views;

namespace TestNote.ViewModels
{
    public partial class EmployeeTestsViewModel
    {

        [RelayCommand]
        private async Task OpenTest(Test test)
        {
            // Se for funcionário e o teste estiver pendente, vai para execução
            await Shell.Current.GoToAsync(nameof(TestExecutionPage), new Dictionary<string, object>
            {
                { "Test", test }
            });
        }
    }
}
