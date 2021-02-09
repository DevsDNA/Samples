namespace AndroidScreenRecorder.Droid
{
    using Android.App;
    using Android.Content;
    using Android.Content.PM;
    using Android.OS;
    using Android.Runtime;
    using AndroidScreenRecorder.Droid.Services;

    [Activity(Label = "AndroidScreenRecorder", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize )]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        public static Activity CurrentActivity { get; private set; }
        public static Intent ReturnDataFromPermission { get; private set; }
        public static Result ReturnResultFromPermission { get; private set; }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            CurrentActivity = this;

            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            if (requestCode == 999 && resultCode == Result.Ok)
            {
                ReturnDataFromPermission = data;
                ReturnResultFromPermission = resultCode;

                Intent startRecordingServiceIntent = new Intent(this, typeof(ScreenRecorderService));
                startRecordingServiceIntent.SetAction("START_SERVICE");
                this.StartService(startRecordingServiceIntent);
            }
            else if (requestCode == 999 && resultCode == Result.Canceled)
            {
                //No tenemos permisos...
            }
            base.OnActivityResult(requestCode, resultCode, data);
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}