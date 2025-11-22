using TestNote.ViewModels;

namespace TestNote.Views;

public partial class EmployeeEditPage : ContentPage
{
    public EmployeeEditPage(EmployeeEditViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}