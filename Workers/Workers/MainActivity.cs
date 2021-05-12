namespace Workers
{
    using Android.App;
    using Android.Icu.Util;
    using Android.OS;
    using Android.Runtime;
    using Android.Widget;
    using AndroidX.AppCompat.App;
    using AndroidX.Lifecycle;
    using AndroidX.Work;
    using global::Workers.Workers;
    using System;
    using System.Linq;

    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity, IObserver
    {
        private WorkManager workerManager;
        private ProgressBar progressBar;
        private TextView textView;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            workerManager = WorkManager.GetInstance(Application.Context);
            workerManager.CancelAllWork();
            workerManager.PruneWork();
            SetContentView(Resource.Layout.activity_main);

            progressBar = FindViewById<ProgressBar>(Resource.Id.progressBar1);
            textView = FindViewById<TextView>(Resource.Id.textView1);
            Button createWork = FindViewById<Button>(Resource.Id.button1);
            createWork.Click += (sender, e) =>
            {
                textView.Text = string.Empty;
                var oneTimeWorkRequest = new OneTimeWorkRequest.Builder(typeof(MyWorker))
               .AddTag("MyWorker")
               .SetInitialDelay(TimeSpan.FromSeconds(10))
               .Build();

                workerManager.BeginUniqueWork("MyWorker", ExistingWorkPolicy.Replace, oneTimeWorkRequest).Enqueue();
            };

            Button createListenableWork = FindViewById<Button>(Resource.Id.button2);
            createListenableWork.Click += (sender, e) =>
            {
                textView.Text = string.Empty;
                var periodicWorkRequest =
                  new PeriodicWorkRequest.Builder(typeof(MyListenableWorker), TimeSpan.FromMinutes(10))
                  .SetBackoffCriteria(BackoffPolicy.Linear, TimeSpan.FromSeconds(10))
                  .AddTag("MyListenableWorker")                  
                  .Build();

                workerManager.Enqueue(periodicWorkRequest);
                workerManager.GetWorkInfosByTagLiveData("MyListenableWorker").Observe(this, this);
            };

            Button cancelListenableWork = FindViewById<Button>(Resource.Id.button3);
            cancelListenableWork.Click += (sender, e) =>
            {
                workerManager.CancelAllWorkByTag("MyListenableWorker");
            };            
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        public void OnChanged(Java.Lang.Object p0)
        {
            var javaList = p0 as JavaList;
            foreach (WorkInfo item in javaList)
            {
                if (item.Tags.Contains("MyListenableWorker"))
                {
                    var state = item.GetState();
                    textView.Text = $"{item.Tags.Last()} > {state.Name()} {textView.Text}";
                    if (state.Name() != "CANCELLED")
                    {
                        progressBar.Progress = item.Progress.GetInt("progress", 0);
                    }
                    else
                    {
                        workerManager.PruneWork();
                    }
                }
            }
        }
    }
}