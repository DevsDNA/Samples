namespace GPRSTracker.Droid.SMS
{
    using Android.App;
    using Android.Content;
    using Android.Runtime;
    using Android.Telephony;
    using GPRSTracker.Models;
    using Java.Lang;
    using Xamarin.Forms;

    [BroadcastReceiver]
    [IntentFilter(new[] { "android.provider.Telephony.SMS_RECEIVED" }, Priority = (int)IntentFilterPriority.HighPriority)]
    public class SMSBroadcastReceiver : BroadcastReceiver
    {
        private const string IntentAction = "android.provider.Telephony.SMS_RECEIVED";
        public override void OnReceive(Context context, Intent intent)
        {
            try
            {
                if (intent.Action == IntentAction)
                {
                    var bundle = intent.Extras;
                    if (bundle != null)
                    {
                        var pdus = bundle.Get("pdus");
                        var castedPdus = JNIEnv.GetArray<Object>(pdus.Handle);
                        var msgs = new SmsMessage[castedPdus.Length];
                        var sb = new StringBuilder();

                        var smsArray = (Java.Lang.Object[])bundle.Get("pdus");
                        string format = bundle.GetString("format");
                        foreach (var item in smsArray)
                        {
                            var sms = SmsMessage.CreateFromPdu((byte[])item, format);
                            SMSMessage sMSMessage = new SMSMessage
                            {
                                From = sms.OriginatingAddress,
                                Message = sms.MessageBody
                            };
                            MessagingCenter.Send<Page, SMSMessage>(App.Current.MainPage, "SMSReceived", sMSMessage);
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
                //Toast.MakeText(context, ex.Message, ToastLength.Long).Show();
            }
        }
    }
}