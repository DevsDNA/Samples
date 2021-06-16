using TestRxUI.Features.Test;
using Xamarin.Forms;

namespace TestRxUI
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new TestView() { ViewModel = new TestViewModel() };
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
