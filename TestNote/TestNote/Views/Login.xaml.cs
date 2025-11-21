namespace TestNote;

public partial class Login : ContentPage
{
	public Login()
	{
		InitializeComponent();
	}

    async void LoginFunction(object sender, EventArgs e)
    {
        // ⚠️ SIMULAÇÃO DO NÍVEL DE ACESSO COM BASE NO EMAIL PARA FINS DE TESTE
        // Na vida real, você faria uma chamada de serviço para autenticar e obter o nível.
        string? email = EmailEntry.Text?.ToLower();
        int accessLevel = 3; // Padrão: Funcionário

        if (email?.Contains("admin") == true)
        {
            accessLevel = 1; // Admin
        }
        else if (email?.Contains("manager") == true)
        {
            accessLevel = 2; // Gerente
        }

        // 1. O admin deve ir para a listagem de Empresas (como o fluxo no diagrama)
        if (accessLevel == 1)
        {
            // Navega para a página de listagem de empresas (Empresas.xaml)
            await Shell.Current.GoToAsync($"//{nameof(Companies)}");
        }
        // 2. O gerente deve ir para a listagem de Empresas, mas com menu "Informação" e "Testes"
        else if (accessLevel == 2)
        {
            // Poderia ir para a mesma Companies, mas a navegação/UI interna seria diferente
            await Shell.Current.GoToAsync($"//{nameof(Companies)}?level=gerente");
        }
        // 3. O funcionário vai direto para os Testes
        else if (accessLevel == 3)
        {
            // O Funcionário não vê empresas, vai direto para a tela de testes.
            // Precisamos criar essa tela: EmployeeTestsView
            // await Shell.Current.GoToAsync($"//{nameof(EmployeeTestsView)}");
            await Shell.Current.DisplayAlert("Acesso", "Funcionário acessa a tela de Testes.", "OK");
        }
        else
        {
            await Shell.Current.DisplayAlert("Erro", "Credenciais inválidas ou nível de acesso desconhecido.", "OK");
        }

        // Ações pós-login:
        EmailEntry.Text = string.Empty;
        PasswordEntry.Text = string.Empty;
    }
}