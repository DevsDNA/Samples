namespace UninstallSample.Droid
{
	using Android.App;
	using Android.Content.PM;
	using Android.OS;
	using Android.Runtime;
    using Plugin.CurrentActivity;
    using Plugin.Permissions;
    using Xamarin.Essentials;
    using Xamarin.Forms;

    [Activity(Label = "UninstallSample", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
		public static Activity CurrentActivity { get; internal set; }

		protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);
			CurrentActivity = this;

			CrossCurrentActivity.Current.Init(this, savedInstanceState);
			Platform.Init(this, savedInstanceState);
            Forms.Init(this, savedInstanceState);
            LoadApplication(new App());
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            //Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
			PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);

			base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}