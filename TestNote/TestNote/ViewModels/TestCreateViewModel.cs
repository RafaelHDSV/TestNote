using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.ComponentModel.Design;
using TestNote.Models;
using TestNote.Services;
using static System.Collections.Specialized.BitVector32;

namespace TestNote.ViewModels
{
    [QueryProperty(nameof(CompanyId), "companyId")]
    public partial class TestCreateViewModel : ObservableObject
    {
        private readonly DatabaseService _dbService;

        [ObservableProperty]
        private int companyId;

        [ObservableProperty]
        private string title = string.Empty;

        [ObservableProperty]
        private string description = string.Empty;

        [ObservableProperty]
        private ObservableCollection<string> items = [];

        [ObservableProperty]
        private string newItemText = string.Empty;

        [ObservableProperty]
        private string section = string.Empty;

        public TestCreateViewModel(DatabaseService dbService)
        {
            _dbService = dbService;
        }

        [RelayCommand]
        private void AddItem()
        {
            if (!string.IsNullOrWhiteSpace(NewItemText))
            {
                Items.Add(NewItemText);
                NewItemText = string.Empty;
            }
        }

        [RelayCommand]
        private void RemoveItem(string item)
        {
            if (Items.Contains(item))
            {
                Items.Remove(item);
            }
        }

        [RelayCommand]
        private async Task SaveTest()
        {
            if (string.IsNullOrWhiteSpace(Title))
            {
                await Shell.Current.DisplayAlert("Erro", "Título é obrigatório", "OK");
                return;
            }

            var newTest = new Test
            {
                CompanyId = CompanyId,
                Title = Title,
                Description = Description,
                TestItems = Items.ToList(),
                Status = "Pendente",
                Section = Section,
                CreatedAt = DateTime.Now
            };

            await _dbService.CreateTestAsync(newTest);
            await Shell.Current.DisplayAlert("Sucesso", "Teste criado!", "OK");
            await Shell.Current.GoToAsync("..");
        }
    }
}