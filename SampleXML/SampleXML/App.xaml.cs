namespace SampleXML
{
	using SampleXML.Features.Main;
	using Xamarin.Forms;

	public partial class App : Application
	{

		public App()
		{
			InitializeComponent();

			MainPage = new MainTabbedView();
		}

		protected override void OnStart()
		{
		}

		protected override void OnSleep()
		{
		}

		protected override void OnResume()
		{
		}
	}
}
