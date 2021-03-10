using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XamarinAccessibility
{
    public partial class App : Application
    {
        public App()
        {
            Device.SetFlags(new string[] { "DragAndDrop_Experimental", "SwipeView_Experimental" });
            InitializeComponent();
            MainPage = new NavigationPage(new MainPage());
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
