using System.Collections.ObjectModel;
using TestNote.Models;
using TestNote.ViewModels;
using TestNote.Views;

namespace TestNote;

public partial class Companies : ContentPage
{
    public Companies()
	{
		InitializeComponent();

        var vm = (CompaniesViewModel)BindingContext;
        vm.LoadCompaniesCommand.ExecuteAsync(null);
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