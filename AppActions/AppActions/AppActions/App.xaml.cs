namespace AppActions
{
    using System.Diagnostics;
    using Xamarin.Essentials;
    using Xamarin.Forms;

    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            Application.Current.UserAppTheme = OSAppTheme.Dark;
            MainPage = new MainPage();
            AppActions.OnAppAction += AppActions_OnAppAction;
        }

        protected override void OnStart()
        {
            try
            {
                AppActions.SetAsync(
                    new AppAction("calendar", "Calendar"),
                    new AppAction("calendar_today", "Today"));
            }
            catch (FeatureNotSupportedException ex)
            {
                Debug.WriteLine("App Actions not supported");
            }
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }

        private void AppActions_OnAppAction(object sender, AppActionEventArgs e)
        {
            if (Application.Current != this && Application.Current is App app)
            {
                AppActions.OnAppAction -= app.AppActions_OnAppAction;
                return;
            }

            //TODO Make the related action
            Debug.WriteLine($"{e.AppAction.Id}");
        }
    }
}