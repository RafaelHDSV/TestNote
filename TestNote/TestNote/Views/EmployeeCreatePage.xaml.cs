using TestNote.ViewModels;

namespace TestNote.Views;

public partial class EmployeeCreatePage : ContentPage
{
    public EmployeeCreatePage(EmployeeCreateViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}