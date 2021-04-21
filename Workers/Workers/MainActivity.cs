namespace Workers
{
    using Android.App;
    using Android.OS;
    using Android.Runtime;
    using AndroidX.AppCompat.App;
    using AndroidX.Work;
    using global::Workers.Workers;
    using System;

    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);

            var periodicWorkRequest = 
                new PeriodicWorkRequest.Builder(typeof(MyListenableWorker)
                ,TimeSpan.FromSeconds(60),TimeSpan.FromSeconds(10))
                .AddTag("MyListenableWorker")
                .Build();

            WorkManager.GetInstance(Application.Context).Enqueue(periodicWorkRequest);

            var oneTimeWorkRequest = new OneTimeWorkRequest.Builder(typeof(MyWorker))
                .AddTag("MyListenableWorker")
                .SetInitialDelay(TimeSpan.FromSeconds(10))
                .Build();

            WorkManager.GetInstance(Application.Context)
                .BeginUniqueWork("MyListenableWorker", ExistingWorkPolicy.Keep
                , oneTimeWorkRequest).Enqueue();
             
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}