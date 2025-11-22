using TestNote.Models;
using TestNote.Services;
using TestNote.Views;

namespace TestNote;

public partial class Login : ContentPage
{
    private readonly DatabaseService _databaseService;

    public Login()
	{
		InitializeComponent();

        var serviceProvider = Application.Current?.Handler?.MauiContext?.Services;
        if (serviceProvider != null)
        {
            _databaseService = serviceProvider.GetService<DatabaseService>()!;
        }
    }

    // ...
    async void LoginFunction(object sender, EventArgs e)
    {
        var user = await _databaseService.LoginAsync(EmailEntry.Text, PasswordEntry.Text);

        if (user == null)
        {
            await DisplayAlert("Erro", "Informações de login incorretos", "OK");
            return;
        }

        if (user.Role == 1) // Admin
        {
            // Admin vê todas as empresas
            await Shell.Current.GoToAsync($"{nameof(Companies)}");
        }
        else if (user.Role == 2) // Manager
        {
            // Precisamos saber QUAL empresa esse gerente cuida
            var managerProfile = await _databaseService.GetManagerByUserIdAsync(user.Id);

            if (managerProfile != null)
            {
                // Vai para uma tela de gestão de funcionários da empresa dele
                // Vamos criar essa tela: ManageEmployeesPage
                await Shell.Current.GoToAsync($"{nameof(ManageEmployeesPage)}?companyId={managerProfile.CompanyId}");
            }
        }
        else if (user.Role == 3) // Employee
        {
            // Employee vê seus testes
            var empProfile = await _databaseService.GetEmployeeByUserIdAsync(user.Id);
            await Shell.Current.DisplayAlert("Ola", $"Funcionario: {empProfile?.Name}", "OK");
            // await Shell.Current.GoToAsync($"{nameof(EmployeeTests)}?employeeId={empProfile.Id}");
        }
    }
}