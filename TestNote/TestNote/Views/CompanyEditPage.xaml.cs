using TestNote.ViewModels;

namespace TestNote.Views;

public partial class CompanyEditPage : ContentPage
{
    public CompanyEditPage(CompanyEditViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}