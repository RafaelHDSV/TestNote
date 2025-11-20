namespace TestNote;

public partial class Login : ContentPage
{
	public Login()
	{
		InitializeComponent();
	}

	async void GoToCompanies(object sender, EventArgs e)
	{
		await Navigation.PushAsync(new Companies());
	}
}