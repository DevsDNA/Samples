[assembly: Xamarin.Forms.Dependency(typeof(AndroidScreenRecorder.Droid.Services.ScreenRecorderService))]
namespace AndroidScreenRecorder.Droid.Services
{
    using Android.App;
    using Android.Content;
    using Android.Hardware.Display;
    using Android.Media;
    using Android.Media.Projection;
    using Android.OS;
    using Android.Runtime;
    using Android.Util;
    using AndroidScreenRecorder.Services;
    using System;

    [Service(ForegroundServiceType = Android.Content.PM.ForegroundService.TypeMediaProjection)]
    public class ScreenRecorderService : Service, IScreenRecordingService
    {
        private static MediaProjection mediaProjection;
        private static MediaRecorder mediaRecorder;
        private static VirtualDisplay recordingDisplay;

        public void AskForStartRecording()
        {
            var manager = MainActivity.CurrentActivity.GetSystemService(Context.MediaProjectionService) as MediaProjectionManager;
            MainActivity.CurrentActivity.StartActivityForResult(manager.CreateScreenCaptureIntent(), 999);
        }

        public override IBinder OnBind(Intent intent)
        {
            throw new NotImplementedException();
        }

        [return: GeneratedEnum]
        public override StartCommandResult OnStartCommand(Intent intent, [GeneratedEnum]StartCommandFlags flags, int startId)
        {
            if (intent.Action == "START_SERVICE")
            {
                RegisterForegroundService();
                StartRecording();
            }
            else if (intent.Action == "STOP_SERVICE")
            {
                StopForeground(true);
                StopSelfResult(startId);
            }
            return StartCommandResult.Sticky;
        }

        public void StartRecording()
        {
            var mManager = MainActivity.CurrentActivity.GetSystemService(Context.MediaProjectionService) as MediaProjectionManager;
            mediaProjection = mManager.GetMediaProjection((int)MainActivity.ReturnResultFromPermission, MainActivity.ReturnDataFromPermission);
            DisplayManager dManager = GetSystemService(Context.DisplayService) as DisplayManager;
            var displayMetrics = new DisplayMetrics();
            dManager.GetDisplay(0).GetMetrics(displayMetrics);

            mediaRecorder = new MediaRecorder();
            mediaRecorder.SetAudioSource(AudioSource.Mic);
            mediaRecorder.SetVideoSource(VideoSource.Surface);

            var profile = CamcorderProfile.Get(CamcorderQuality.High);
            profile.FileFormat = OutputFormat.Mpeg4;
            profile.VideoFrameHeight = displayMetrics.HeightPixels;
            profile.VideoFrameWidth = displayMetrics.WidthPixels;

            mediaRecorder.SetProfile(profile);
            mediaRecorder.SetOutputFile($"{Android.OS.Environment.ExternalStorageDirectory}/demovideo.mp4");
            mediaRecorder.Prepare();

            recordingDisplay = mediaProjection.CreateVirtualDisplay("Rec display", displayMetrics.WidthPixels, displayMetrics.HeightPixels,
                                                                    (int)displayMetrics.Density, Android.Views.DisplayFlags.Round,
                                                                    mediaRecorder.Surface, null, null);

            mediaRecorder.Start();
        }

        public void StopRecording()
        {
            mediaRecorder.Stop();
            mediaProjection.Stop();
            recordingDisplay.Release();

            Intent stopIntent = new Intent(MainActivity.CurrentActivity, this.Class);
            stopIntent.SetAction("STOP_SERVICE");
            MainActivity.CurrentActivity.StartService(stopIntent);
        }

        private void RegisterForegroundService()
        {
            NotificationChannel chan = new NotificationChannel(
              "videoChannel",
              "video channel foreground service",
              NotificationImportance.Low);

            NotificationManager manager = (NotificationManager)MainActivity.CurrentActivity.GetSystemService(Context.NotificationService);
            manager.CreateNotificationChannel(chan);

            Notification notification = new Notification.Builder(this, "videoChannel")
                .SetContentTitle("RecordingPoC")
                .SetSmallIcon(Resource.Drawable.abc_ic_star_black_16dp)
                .SetOngoing(true)
                .Build();

            StartForeground(125, notification);
        }
    }
}