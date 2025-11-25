using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using TestNote.Models;
using TestNote.Services;

namespace TestNote.ViewModels
{
    public partial class AdminUsersViewModel : ObservableObject
    {
        private readonly DatabaseService _dbService;

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

            var users = await _dbService.GetAllUsersAsync();

            AllUsers = new ObservableCollection<User>(users);
            IsLoading = false;
        }

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