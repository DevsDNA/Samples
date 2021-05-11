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
                await Task.Delay(TimeSpan.FromSeconds(10));
                SetProgressAsync(new Data.Builder().PutInt("progress", 0).Build());
                await Task.Delay(TimeSpan.FromSeconds(10));
                SetProgressAsync(new Data.Builder().PutInt("progress", 50).Build());
                await Task.Delay(TimeSpan.FromSeconds(10));
                SetProgressAsync(new Data.Builder().PutInt("progress", 100).Build());

                return p0.Set(Result.InvokeSuccess());
            });
            return "MyListenableWorker";
        }

        public override IListenableFuture StartWork()
        {
            return CallbackToFutureAdapter.GetFuture(this);
        }
    }
}