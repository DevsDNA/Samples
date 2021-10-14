namespace DynamicDataExample
{
    using DynamicDataExample.Features;
    using Xamarin.Forms;

    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainPage = new DynamicDataView() { ViewModel = new DynamicDataViewModel() };
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
