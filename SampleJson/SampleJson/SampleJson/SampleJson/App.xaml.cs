namespace SampleJson
{
	using SampleJson.Features.Main;
	using Xamarin.Forms;

	public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new MainView();
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
