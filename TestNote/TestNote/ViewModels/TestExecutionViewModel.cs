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

        [ObservableProperty]
        private ObservableCollection<CheckItem> checkItems = [];

        [ObservableProperty]
        private string executionNotes = string.Empty;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(CompletionProgress))]
        private int completedCount;

        public int TotalCount => CheckItems.Count;
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
                var completedString = value.CompletedItemsString ?? string.Empty;
                var completedList = completedString.Split('|', StringSplitOptions.RemoveEmptyEntries).ToList();

                foreach (var item in value.TestItems)
                {
                    CheckItems.Add(new CheckItem
                    {
                        Description = item,
                        IsChecked = completedList.Contains(item)
                    });
                }
                ExecutionNotes = value.ExecutionNotes;
                UpdateProgress();
                OnPropertyChanged(nameof(TotalCount));
            }
        }

        [RelayCommand]
        private void ItemToggled()
        {
            UpdateProgress();
        }

        public void UpdateProgress()
        {
            CompletedCount = CheckItems.Count(i => i.IsChecked);
        }

        [RelayCommand]
        private async Task CompleteTest()
        {
            if (Test == null) return;

            int currentChecked = CheckItems.Count(i => i.IsChecked);
            int total = CheckItems.Count;

            Test.ExecutionNotes = ExecutionNotes;
            Test.CompletedItemCount = currentChecked;

            if (currentChecked >= total && total > 0)
            {
                Test.Status = "Concluído";
            }
            else if (currentChecked > 0)
            {
                Test.Status = "Em Andamento";
            }
            else
            {
                Test.Status = "Pendente";
            }

            Test.TestedAt = DateTime.Now;

            var completedDescriptions = CheckItems
                .Where(i => i.IsChecked)
                .Select(i => i.Description);

            Test.CompletedItemsString = string.Join("|", completedDescriptions);

            await _dbService.SaveItemAsync(Test);

            await Shell.Current.DisplayAlert("Sucesso", $"Teste salvo.\nStatus: {Test.Status}", "OK");
            await Shell.Current.GoToAsync("..");
        }
    }

    public partial class CheckItem : ObservableObject 
    {
        [ObservableProperty]
        private string description = string.Empty;

        [ObservableProperty]
        private bool isChecked;
    }
}