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

        [ObservableProperty]
        private string executionNotes = string.Empty;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(CompletionProgress))]
        private int completedCount;

        public double CompletionProgress => (double)CompletedCount / Math.Max(1, CheckItems.Count);

        public TestExecutionViewModel(DatabaseService dbService)
        {
            _dbService = dbService;
        }

        partial void OnTestChanged(Test value)
        {
            if (value != null)
            {
                CheckItems.Clear();
                var completedList = value.CompletedItemsString.Split('|', StringSplitOptions.RemoveEmptyEntries).ToList();

                foreach (var item in value.TestItems)
                {
                    CheckItems.Add(new CheckItem
                    {
                        Description = item,
                        IsChecked = completedList.Contains(item) // Verifica se já estava concluído
                    });
                }
                ExecutionNotes = value.ExecutionNotes;
                UpdateProgress(); // Calcula o progresso inicial
            }
        }

        [RelayCommand]
        private void ItemToggled()
        {
            // Chamado quando um Checkbox é alterado
            UpdateProgress();
        }

        private void UpdateProgress()
        {
            CompletedCount = CheckItems.Count(i => i.IsChecked);
        }

        [RelayCommand]
        private async Task CompleteTest()
        {
            if (Test == null) return;

            // 1. Atualiza o Teste com novos dados
            Test.ExecutionNotes = ExecutionNotes;
            Test.CompletedItemCount = CompletedCount;
            Test.Status = CompletedCount == CheckItems.Count ? "Concluído" : "Em Andamento";
            Test.TestedAt = DateTime.Now;

            // 2. Serializa itens concluídos de volta para o modelo
            var completedDescriptions = CheckItems
                .Where(i => i.IsChecked)
                .Select(i => i.Description);

            Test.CompletedItemsString = string.Join("|", completedDescriptions);

            // 3. Salva no banco
            await _dbService.SaveItemAsync(Test);

            await Shell.Current.DisplayAlert("Sucesso", $"Teste atualizado. Status: {Test.Status}", "OK");
            await Shell.Current.GoToAsync("..");
        }
    }

    // Classe auxiliar para a tela
    public partial class CheckItem : ObservableObject // Se estiver usando CommunityToolkit.MVVM
    {
        [ObservableProperty]
        private string description = string.Empty;

        [ObservableProperty]
        private bool isChecked;
    }
}