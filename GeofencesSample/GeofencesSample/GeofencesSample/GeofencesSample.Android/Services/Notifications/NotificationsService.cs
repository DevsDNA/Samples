namespace GeofencesSample.Droid.Services.Notifications
{
    using Android;
    using Android.App;
    using Android.Content;
    using Android.Graphics;
    using System;

    internal class NotificationsService
	{
		private Context context;

		public NotificationsService(Context context)
		{
			this.context = context;
		}


		public void SendNotification(string title, string text, long when)
		{
			try
			{
				Intent intent = new Intent(context, typeof(MainActivity));
				intent.AddFlags(ActivityFlags.ClearTop);
				PendingIntent pendingIntent = PendingIntent.GetActivity(context, 0, intent, PendingIntentFlags.OneShot);
				Notification notification = CreateNotification(title, text, when, pendingIntent);
				NotificationManager notificationManager = NotificationManager.FromContext(context);
				// Notification channels are new in API 26 (and not a part of the
				// support library). There is no need to create a notification
				// channel on older versions of Android.
				CreateAlertChannel(notificationManager);
				notificationManager.Notify(0, notification);
			}
			catch (Exception ex)
			{
				System.Diagnostics.Debug.WriteLine(ex.Message);
			}
		}

		private Notification CreateNotification(string title, string text, long when, PendingIntent pendingIntent)
		{
			Notification.Builder notificationBuilder = new Notification.Builder(context, "test")
								.SetSmallIcon(Resource.Drawable.IcNotificationOverlay)
								.SetLargeIcon(BitmapFactory.DecodeResource(context.Resources, Resource.Drawable.IcNotificationOverlay))
								.SetContentTitle(title)
								.SetContentText(text)
								.SetWhen(when)
								.SetVisibility(NotificationVisibility.Public)
								.SetAutoCancel(true)
								.SetContentIntent(pendingIntent)
								.SetColor(new Color(88, 193, 253));
			return notificationBuilder.Build();

		}

		private void CreateAlertChannel(NotificationManager notificationManager)
		{
			NotificationChannel channel = new NotificationChannel("test", "TEST", NotificationImportance.High)
			{
				Description = "Notifications for tests.",
				LockscreenVisibility = NotificationVisibility.Public
			};
			notificationManager.CreateNotificationChannel(channel);
		}
	}
}