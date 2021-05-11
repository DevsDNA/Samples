namespace Workers
{
    using Android.App;
    using Android.OS;
    using Android.Runtime;
    using Android.Widget;
    using AndroidX.AppCompat.App;
    using AndroidX.Lifecycle;
    using AndroidX.Work;
    using global::Workers.Workers;
    using Java.Lang;
    using Java.Util.Concurrent;
    using System;
    using System.Collections.Generic;

    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity, IObserver, IRunnable, IExecutor
    {
        private ProgressBar progressBar;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            var workerManager = WorkManager.GetInstance(Application.Context);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
            //EditText phoneNumberText = FindViewById<EditText>(Resource.Id.);
            Button createWork = FindViewById<Button>(Resource.Id.button1);
            createWork.Click += (sender, e) =>
            {
                var oneTimeWorkRequest = new OneTimeWorkRequest.Builder(typeof(MyWorker))
               .AddTag("MyListenableWorker")
               .SetInitialDelay(TimeSpan.FromSeconds(10))
               .Build();

                workerManager
                    .BeginUniqueWork("MyListenableWorker", ExistingWorkPolicy.Keep
                    , oneTimeWorkRequest).Enqueue();
            };

            Button createListenableWork = FindViewById<Button>(Resource.Id.button2);
            createListenableWork.Click += (sender, e) =>
            {
                var periodicWorkRequest =
                  new PeriodicWorkRequest.Builder(typeof(MyListenableWorker), TimeSpan.FromSeconds(1))
                  .AddTag("MyListenableWorker")
                  .Build();

                workerManager.Enqueue(periodicWorkRequest);
                workerManager.GetWorkInfosByTagLiveData("MyListenableWorker").Observe(this, this);
            };

            Button cancelListenableWork = FindViewById<Button>(Resource.Id.button3);
            cancelListenableWork.Click += (sender, e) =>
            {
                var data = workerManager.GetWorkInfosByTag("MyListenableWorker");

                workerManager.CancelAllWorkByTag("MyListenableWorker");
            };
            progressBar = FindViewById<ProgressBar>(Resource.Id.progressBar1);
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        public void OnChanged(Java.Lang.Object p0)
        {
            var javaList = (p0 as JavaList);
            foreach (var javaItem in javaList)
            {
                var workInfo = (WorkInfo)javaItem;
                if (workInfo.Tags.Contains("MyListenableWorker"))
                {
                    var state = workInfo.GetState();
                    if (state.Name() == "CANCELLED")
                    {

                    }
                    else
                    {

                    }
                    
                }
            }
        }

        public void Run()
        {
        }

        public void Execute(IRunnable command)
        {
        }
    }
}