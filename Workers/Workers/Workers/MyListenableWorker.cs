namespace Workers.Workers
{
    using Android.Content;
    using AndroidX.Concurrent.Futures;
    using AndroidX.Work;
    using Google.Common.Util.Concurrent;
    using System;
    using System.Threading.Tasks;

    public class MyListenableWorker : ListenableWorker, CallbackToFutureAdapter.IResolver
    {
        public MyListenableWorker(Context appContext, WorkerParameters workerParams) : base(appContext, workerParams)
        {
        }

        public Java.Lang.Object AttachCompleter(CallbackToFutureAdapter.Completer p0)
        {
            Task.Run(async () =>
            {
                for (int i = 0; i < 20; i++)
                {
                    await Task.Delay(TimeSpan.FromSeconds(1));
                    SetProgressAsync(new Data.Builder().PutInt("progress", i*5).Build());
                }
                return p0.Set(Result.InvokeRetry());
            });
            return "MyListenableWorker";
        }

        public override IListenableFuture StartWork()
        {
            return CallbackToFutureAdapter.GetFuture(this);
        }
    }
}