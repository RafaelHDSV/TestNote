using TestNote.ViewModels;

namespace TestNote.Views;

public partial class TestExecutionPage : ContentPage
{
	public TestExecutionPage(TestExecutionViewModel vm)
	{
		InitializeComponent();
        BindingContext = vm;
    }

    public void OnCheckBoxChanged(object sender, CheckedChangedEventArgs e)
    {
        if (BindingContext is TestExecutionViewModel vm)
        {
            vm.UpdateProgress();
        }
    }
}