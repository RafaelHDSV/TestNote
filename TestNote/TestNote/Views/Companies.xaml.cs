using System.Collections.ObjectModel;
using TestNote.Models;
using TestNote.ViewModels;
using TestNote.Views;
using Microsoft.Maui.Controls;

namespace TestNote;

public partial class Companies : ContentPage
{
    public Companies(CompaniesViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        // 1. Acessa o ViewModel
        if (BindingContext is CompaniesViewModel viewModel)
        {
            // 2. Executa o comando de forma assíncrona.
            // Isso chama LoadCompaniesAsync() no seu ViewModel.
            await viewModel.LoadCompaniesCommand.ExecuteAsync(null);
        }
    }

    private async void OnCompanySelected(object sender, SelectionChangedEventArgs e)
    {
        var company = e.CurrentSelection.FirstOrDefault() as Company;
        if (company == null)
            return;

        // Limpa a seleção
        ((CollectionView)sender).SelectedItem = null;

        // Navegar para os detalhes
        await Shell.Current.GoToAsync(nameof(CompanyDetails), new Dictionary<string, object>
        {
            { "Company", company }
        });
    }
}