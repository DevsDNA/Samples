[assembly: Xamarin.Forms.Dependency(typeof(GPRSTracker.Droid.SMS.SMSService))]
namespace GPRSTracker.Droid.SMS
{
    using Android.App;
    using Android.Content;
    using Android.Telephony;
    using GPRSTracker.Models;
    using GPRSTracker.Services;

    public class SMSService : ISMS
    {
        public void Send(SMSMessage sMSMessage)
        {
            var pendingIntent = PendingIntent.GetActivity(Application.Context, 0, new Intent(Application.Context, typeof(MainActivity)).AddFlags(ActivityFlags.ClearTop | ActivityFlags.NewTask), PendingIntentFlags.NoCreate);

            SmsManager smsM = SmsManager.Default;
            smsM.SendTextMessage(sMSMessage.To, null, sMSMessage.Message, pendingIntent, null);
        }
    }
}