using TestNote.ViewModels;

namespace TestNote.Views;

public partial class TestExecutionPage : ContentPage
{
	public TestExecutionPage(TestExecutionViewModel vm)
	{
		InitializeComponent();
        BindingContext = vm;
    }
}