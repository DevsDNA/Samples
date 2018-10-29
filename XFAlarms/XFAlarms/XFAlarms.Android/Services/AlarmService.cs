[assembly: Xamarin.Forms.Dependency(typeof(XFAlarms.Droid.Services.AlarmService))]
namespace XFAlarms.Droid.Services
{
    using Android;
    using Android.App;
    using Android.Content;
    using Android.Content.PM;
    using Android.Database;
    using Android.OS;
    using Android.Provider;
    using Android.Support.V4.App;
    using Android.Support.V4.Content;
    using Java.Util;
    using System;
    using System.Threading.Tasks;
    using XFAlarms.Services;

    public class AlarmService : IAlarmService
    {
        private int defaultCalendarId;
        private string[] calendarsProjection =
        {
            CalendarContract.Calendars.InterfaceConsts.Id,
            CalendarContract.Calendars.InterfaceConsts.CalendarDisplayName,
            CalendarContract.Calendars.InterfaceConsts.AccountName
        };
        private string[] eventProjection =
        {
            CalendarContract.Events.InterfaceConsts.Id
        };

        public AlarmService()
        {
            RequestAppPermissions();
        }

        public Task<bool> CheckIfAlarmAlreadyExistAsync(string id)
        {
            TaskCompletionSource<bool> tcs = new TaskCompletionSource<bool>();
            var alarmUri = ContentUris.AppendId(CalendarContract.Events.ContentUri.BuildUpon(), long.Parse(id));
            var cursor = Application.Context.ContentResolver.Query(alarmUri.Build(), eventProjection, null, null);
            if (cursor.Count == 0)
                tcs.SetResult(false);
            else
                tcs.SetResult(true);

            return tcs.Task;
        }

        public Task<string> CreateAlarmAsync(string title, string description, DateTime timeInit, DateTime timeEnd, int alarmMinutes)
        {
            TaskCompletionSource<string> tcs = new TaskCompletionSource<string>();
            GetSystemCalendar();
            if (defaultCalendarId == 0)
            {
                tcs.SetResult(string.Empty);
                return tcs.Task;
            }

            ContentValues eventValues = new ContentValues();
            eventValues.Put(CalendarContract.Events.InterfaceConsts.CalendarId, defaultCalendarId);
            eventValues.Put(CalendarContract.Events.InterfaceConsts.Title, title);
            eventValues.Put(CalendarContract.Events.InterfaceConsts.Description, description);
            eventValues.Put(CalendarContract.Events.InterfaceConsts.Dtstart, GetDateTimeMS(timeInit));
            eventValues.Put(CalendarContract.Events.InterfaceConsts.Dtend, GetDateTimeMS(timeEnd));
            eventValues.Put(CalendarContract.Events.InterfaceConsts.EventTimezone, System.TimeZone.CurrentTimeZone.StandardName);
            eventValues.Put(CalendarContract.Events.InterfaceConsts.EventEndTimezone, System.TimeZone.CurrentTimeZone.StandardName);
            var uri = Application.Context.ContentResolver.Insert(CalendarContract.Events.ContentUri, eventValues);
            if (!long.TryParse(uri.LastPathSegment, out long eventID))
                tcs.SetResult(string.Empty);
            else
            {
                ContentValues reminderValues = new ContentValues();
                reminderValues.Put(CalendarContract.Reminders.InterfaceConsts.Minutes, alarmMinutes);
                reminderValues.Put(CalendarContract.Reminders.InterfaceConsts.EventId, eventID);
                reminderValues.Put(CalendarContract.Reminders.InterfaceConsts.Method, (int)RemindersMethod.Alarm);
                uri = Application.Context.ContentResolver.Insert(CalendarContract.Reminders.ContentUri, reminderValues);
                tcs.SetResult(eventID.ToString());
            }

            return tcs.Task;
        }

        public Task<bool> CreateCalendarForAppAlarmsAsync()
        {
            return null;
        }

        public Task<bool> DeleteAlarmAsync(string id)
        {
            TaskCompletionSource<bool> tcs = new TaskCompletionSource<bool>();
            var deleteUri = ContentUris.AppendId(CalendarContract.Events.ContentUri.BuildUpon(), long.Parse(id));
            int rows = Application.Context.ContentResolver.Delete(deleteUri.Build(), null, null);
            if (rows == 0)
                tcs.SetResult(false);
            else
                tcs.SetResult(true);

            return tcs.Task;
        }

        long GetDateTimeMS(DateTime date)
        {
            Calendar calendar = Calendar.GetInstance(Java.Util.TimeZone.GetTimeZone(System.TimeZone.CurrentTimeZone.StandardName));
            calendar.Set(CalendarField.DayOfMonth, date.Day);
            calendar.Set(CalendarField.Month, date.Month -1);
            calendar.Set(CalendarField.Year, date.Year);
            calendar.Set(CalendarField.HourOfDay, date.Hour);
            calendar.Set(CalendarField.Minute, date.Minute);

            return calendar.TimeInMillis;
        }

        private void GetSystemCalendar()
        {
            var calendarsUri = CalendarContract.Calendars.ContentUri;
            var loader = new Android.Support.V4.Content.CursorLoader(Application.Context, calendarsUri, calendarsProjection, null, null, null);
            var cursor = (ICursor)loader.LoadInBackground();
            cursor.MoveToLast();
            defaultCalendarId = cursor.GetInt(cursor.GetColumnIndex(calendarsProjection[0]));
        }

        private static void RequestAppPermissions()
        {
            if (Build.VERSION.SdkInt < BuildVersionCodes.Lollipop)
                return;

            if (HasPermissions())
                return;

            ActivityCompat.RequestPermissions(MainActivity.CurrentActivity, new string[] 
            {
                Manifest.Permission.WriteCalendar,
                Manifest.Permission.ReadCalendar
            }, 1000);
        }

        private static bool HasPermissions()
        {
            return (ContextCompat.CheckSelfPermission(Application.Context, Manifest.Permission.WriteCalendar) == Permission.Granted) &&
                   (ContextCompat.CheckSelfPermission(Application.Context, Manifest.Permission.ReadCalendar) == Permission.Granted);
        }
    }
}