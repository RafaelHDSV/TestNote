using System.Collections.ObjectModel;
using TestNote.Models;
using TestNote.ViewModels;

namespace TestNote;

public partial class Companies : ContentPage
{
    public Companies()
	{
		InitializeComponent();

        var vm = (CompaniesViewModel)BindingContext;
        vm.LoadCompaniesCommand.ExecuteAsync(null);
    }
}