using TestNote.ViewModels;

namespace TestNote.Views;

public partial class ManagerCreatePage : ContentPage
{
    public ManagerCreatePage(ManagerCreateViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}