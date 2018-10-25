[assembly: Xamarin.Forms.Dependency(typeof(XFAlarms.Droid.Services.AlarmService))]
namespace XFAlarms.Droid.Services
{
    using Android.Content;
    using Android.Database;
    using Android.Provider;
    using Java.Util;
    using System;
    using System.Threading.Tasks;
    using XFAlarms.Services;

    public class AlarmService : IAlarmService
    {
        private int defaultCalendarId;

        public AlarmService()
        {
            var calendarsUri = CalendarContract.Calendars.ContentUri;
            string[] calendarsProjection = 
            {
                CalendarContract.Calendars.InterfaceConsts.Id,
                CalendarContract.Calendars.InterfaceConsts.CalendarDisplayName,
                CalendarContract.Calendars.InterfaceConsts.AccountName
            };

            var loader = new CursorLoader(Android.App.Application.Context, calendarsUri, calendarsProjection, null, null, null);
            var cursor = (ICursor)loader.LoadInBackground();
            defaultCalendarId = cursor.GetInt(cursor.GetColumnIndex(calendarsProjection[0]));
        }

        public Task<bool> CheckIfAlarmAlreadyExistAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<string> CreateAlarmAsync(string title, string description, DateTime timeInit, DateTime timeEnd, int alarmMinutes)
        {
            TaskCompletionSource<string> tcs = new TaskCompletionSource<string>();

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
            eventValues.Put(CalendarContract.Events.InterfaceConsts.EventTimezone, "UTC");
            eventValues.Put(CalendarContract.Events.InterfaceConsts.EventEndTimezone, "UTC");
            eventValues.Put(CalendarContract.Events.InterfaceConsts.HasAlarm, true);
            return tcs.Task;
        }

        public Task<bool> CreateCalendarForAppAlarmsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAlarmAsync(string id)
        {
            throw new NotImplementedException();
        }

        long GetDateTimeMS(DateTime date)
        {
            Calendar calendar = Calendar.GetInstance(Java.Util.TimeZone.Default);
            calendar.Set(CalendarField.DayOfMonth, date.Day);
            calendar.Set(CalendarField.Month, date.Month);
            calendar.Set(CalendarField.Year, date.Year);
            calendar.Set(CalendarField.HourOfDay, date.Hour);
            calendar.Set(CalendarField.Minute, date.Minute);

            return calendar.TimeInMillis;
        }
    }
}