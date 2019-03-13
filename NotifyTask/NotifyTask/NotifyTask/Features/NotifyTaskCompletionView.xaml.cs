namespace NotifyTask.Features
{
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;

    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class NotifyTaskCompletionView : ContentPage
	{
		public NotifyTaskCompletionView ()
		{
			InitializeComponent ();
            BindingContext = new NotifyTaskCompletionViewModel();
		}

	}
}