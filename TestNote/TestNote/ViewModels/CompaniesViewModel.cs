using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestNote.Models;
using TestNote.Services;

namespace TestNote.ViewModels
{
    public partial class CompaniesViewModel : ObservableObject
    {
        [ObservableProperty]
        public ObservableCollection<Company> companies = [];

        [ObservableProperty]
        public bool isLoading; // Corrigido para Campo Privado

        private readonly DatabaseService _databaseService;

        public CompaniesViewModel(DatabaseService databaseService)
        {
            _databaseService = databaseService;
            LoadCompaniesCommand = new AsyncRelayCommand(LoadCompaniesAsync);
        }

        public CompaniesViewModel() : this(null!)
        {
        }

        public IAsyncRelayCommand LoadCompaniesCommand { get; }

        private async Task LoadCompaniesAsync()
        {
            IsLoading = true;

            if (_databaseService != null)
            {
                var data = await _databaseService.GetItemsAsync<Company>();
                Companies = new ObservableCollection<Company>(data);
            }

            IsLoading = false;
        }
    }
}
