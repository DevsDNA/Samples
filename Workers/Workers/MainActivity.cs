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
                var oneTimeWorkRequest = new OneTimeWorkRequest.Builder(typeof(MyWorker))
               .AddTag("MyListenableWorker")
               .SetInitialDelay(TimeSpan.FromSeconds(10))
               .Build();

                workerManager.BeginUniqueWork("MyListenableWorker", ExistingWorkPolicy.Keep, oneTimeWorkRequest).Enqueue();
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
                    textView.Text = $"{workInfo.Tags.Last()} > {state.Name()} | {textView.Text}";
                    if (state.Name() != "CANCELLED")
                    {
                        progressBar.Progress = workInfo.Progress.GetInt("progress", 0);
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