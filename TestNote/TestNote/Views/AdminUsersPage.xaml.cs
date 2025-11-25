using TestNote.ViewModels;

namespace TestNote.Views;

public partial class AdminUsersPage : ContentPage
{
	public AdminUsersPage(AdminUsersViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        if (BindingContext is AdminUsersViewModel vm)
        {
            await vm.LoadAllUsersAsync();
        }
    }
}