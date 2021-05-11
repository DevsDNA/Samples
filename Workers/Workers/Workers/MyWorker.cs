namespace Workers.Workers
{
    using Android.Content;
    using AndroidX.Work;
    using System.Diagnostics;

    public class MyWorker : Worker
    {
        public MyWorker(Context context, WorkerParameters workerParams) : base(context, workerParams)
        {
        }

        public override Result DoWork()
        {
            Debug.WriteLine("My Worker");
            
            return Result.InvokeSuccess();
        }
    }
}