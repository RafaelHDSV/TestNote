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
            _ = _databaseService.FixMissingDataAsync();
        }
    }

    async void LoginFunction(object sender, EventArgs e)
    {
        var user = await _databaseService.LoginAsync(EmailEntry.Text, PasswordEntry.Text);

        if (user != null)
        {
            UserSession.CurrentUser = user;
        }

        if (user == null)
        {
            await DisplayAlert("Erro", "Informações de login incorretos", "OK");
            return;
        }

        if (user.Role == 1)
        {
            await Shell.Current.GoToAsync($"{nameof(Companies)}");
        }
        else if (user.Role == 2)
        {
            var managerProfile = await _databaseService.GetManagerByUserIdAsync(user.Id);

            if (managerProfile != null)
            {
                var company = await _databaseService.GetItemAsync<Company>(managerProfile.CompanyId);

                if (company != null)
                {
                    await Shell.Current.GoToAsync(nameof(CompanyDetails), new Dictionary<string, object>
            {
                { "Company", company }
            });
                }
                else
                {
                    await DisplayAlert("Erro", "Empresa vinculada não encontrada.", "OK");
                }
            }
            else
            {
                await DisplayAlert("Erro", "Perfil de gerente não encontrado.", "OK");
            }
        }
        else if (user.Role == 3)
        {
            var empProfile = await _databaseService.GetEmployeeByUserIdAsync(user.Id);

            if (empProfile != null)
            {
                var company = await _databaseService.GetItemAsync<Company>(empProfile.CompanyId);

                if (company != null)
                {
                    await Shell.Current.GoToAsync(nameof(CompanyTests), new Dictionary<string, object>
                      {
                        { "Company", company }
                      });
                }
                else
                {
                    await DisplayAlert("Erro de Acesso", "Empresa vinculada não encontrada para seu perfil.", "OK");
                }
            }
            else
            {
                await DisplayAlert("Erro de Acesso", "Perfil de funcionário não encontrado.", "OK");
            }
        }
    }
}