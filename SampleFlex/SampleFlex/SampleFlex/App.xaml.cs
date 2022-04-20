namespace SampleFlex
{
	using Xamarin.Forms;

	public partial class App : Application
	{

		public App()
		{
			InitializeComponent();

			var tabbedPage = new TabbedPage();
			tabbedPage.Children.Add(new MainPage() { Title = "Propiedades" });
			tabbedPage.Children.Add(new SampleScreen() { Title = "Ejemplo" });
			MainPage = tabbedPage;
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
