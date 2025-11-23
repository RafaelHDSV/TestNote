using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using TestNote.Models;
using TestNote.Services;

namespace TestNote.ViewModels
{
    [QueryProperty(nameof(Test), "Test")]
    public partial class TestExecutionViewModel : ObservableObject
    {
        private readonly DatabaseService _dbService;

        [ObservableProperty]
        private Test test;

        // Lista de itens para Checkbox (precisamos de um model auxiliar para controle de estado na tela)
        [ObservableProperty]
        private ObservableCollection<CheckItem> checkItems = [];

        public TestExecutionViewModel(DatabaseService dbService)
        {
            _dbService = dbService;
        }

        partial void OnTestChanged(Test value)
        {
            if (value != null)
            {
                // Carrega os itens do teste
                CheckItems.Clear();
                foreach (var item in value.TestItems)
                {
                    CheckItems.Add(new CheckItem { Description = item, IsChecked = false });
                }
            }
        }

        [RelayCommand]
        private async Task CompleteTest()
        {
            // Validação: Todos os itens marcados?
            if (CheckItems.Any(i => !i.IsChecked))
            {
                bool confirm = await Shell.Current.DisplayAlert("Atenção", "Nem todos os itens foram marcados. Deseja finalizar mesmo assim?", "Sim", "Não");
                if (!confirm) return;
            }

            // Atualiza o Teste
            Test.Status = "Concluído"; // Ou "Em Revisão"
            Test.TestedAt = DateTime.Now;
            // Test.ExecutorId = ... (Pegar ID do usuário logado, se tiver sessão global)

            await _dbService.SaveItemAsync(Test);
            await Shell.Current.DisplayAlert("Sucesso", "Teste finalizado!", "OK");
            await Shell.Current.GoToAsync("..");
        }
    }

    // Classe auxiliar para a tela
    public class CheckItem
    {
        public string Description { get; set; } = string.Empty;
        public bool IsChecked { get; set; }
    }
}