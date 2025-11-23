using TestNote.ViewModels;

namespace TestNote.Views;

public partial class TestCreatePage : ContentPage
{
    public TestCreatePage(TestCreateViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}