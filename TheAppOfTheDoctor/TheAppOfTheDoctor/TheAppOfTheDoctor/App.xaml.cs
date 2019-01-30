[assembly: Xamarin.Forms.Xaml.XamlCompilation(Xamarin.Forms.Xaml.XamlCompilationOptions.Compile)]
namespace TheAppOfTheDoctor
{
    using Microsoft.AppCenter;
    using Microsoft.AppCenter.Analytics;
    using Microsoft.AppCenter.Crashes;
    using TheAppOfTheDoctor.Environments;
    using TheAppOfTheDoctor.Features.Doctor;
    using Xamarin.Forms;



    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new DoctorDescriptionView
            {
                BindingContext = new DoctorDescriptionViewModel()
            };
        }

        protected override void OnStart()
        {
            AppCenter.Start($"android={AppInfo.AppCenterIdDROID};ios={AppInfo.AppCenterIdIOS}",
                  typeof(Analytics), typeof(Crashes));

            Analytics.TrackEvent("OnStart");
        }

        protected override void OnSleep()
        {
            Analytics.TrackEvent("OnSleep");
        }

        protected override void OnResume()
        {
            Analytics.TrackEvent("OnResume");
        }
    }
}
