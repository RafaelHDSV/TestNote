using TestNote.ViewModels;

namespace TestNote.Views;

public partial class CompanyCreatePage : ContentPage
{
    public CompanyCreatePage(CompanyCreateViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}