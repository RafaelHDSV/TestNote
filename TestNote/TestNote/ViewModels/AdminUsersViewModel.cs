using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using TestNote.Models;
using TestNote.Services;

namespace TestNote.ViewModels
{
    // Tornar partial para usar o NotifyPropertyChange e o ObservableProperty
    public partial class AdminUsersViewModel : ObservableObject
    {
        private readonly DatabaseService _dbService;

        // ⚠️ Agora esta lista é de objetos User
        [ObservableProperty]
        private ObservableCollection<User> allUsers = [];

        [ObservableProperty]
        private bool isLoading;

        public AdminUsersViewModel(DatabaseService dbService)
        {
            _dbService = dbService;
        }

        public async Task LoadAllUsersAsync()
        {
            IsLoading = true;

            // ⚠️ Chama o novo método que busca a tabela User diretamente
            var users = await _dbService.GetAllUsersAsync();

            AllUsers = new ObservableCollection<User>(users);
            IsLoading = false;
        }

        // Método auxiliar para conversão na View
        public string GetRoleName(int role)
        {
            return role switch
            {
                1 => "Administrador",
                2 => "Gerente",
                3 => "Funcionário",
                _ => "Desconhecido",
            };
        }
    }
}