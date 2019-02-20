namespace TheAppOfTheDoctor.Droid
{

    using Android.App;
    using Android.Content;
    using Android.OS;
    using Android.Util;

    [Activity(Theme = "@style/MyTheme.Splash", MainLauncher = true, NoHistory = true)]
    public class SplashActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        static readonly string TAG = "X:" + typeof(SplashActivity).Name;

        public override void OnCreate(Bundle savedInstanceState, PersistableBundle persistentState)
        {
            base.OnCreate(savedInstanceState, persistentState);
            Log.Debug(TAG, "SplashActivity.OnCreate");
        }

        // Launches the startup task
        protected override void OnResume()
        {
            base.OnResume();

            Log.Debug(TAG, "Startup work is finished - starting MainActivity.");
            StartActivity(new Intent(Application.Context, typeof(MainActivity)));
        }
    }
}