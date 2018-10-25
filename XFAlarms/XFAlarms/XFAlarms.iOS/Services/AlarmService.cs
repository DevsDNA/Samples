[assembly: Xamarin.Forms.Dependency(typeof(XFAlarms.iOS.Services.AlarmService))]
namespace XFAlarms.iOS.Services
{
    using EventKit;
    using Foundation;
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using UIKit;
    using Xamarin.Forms.Platform.iOS;
    using XFAlarms.Services;

    public class AlarmService : IAlarmService
    {
        private string calendarTitle = "My App Calendar";
        private static EKCalendar appCalendar;
        private static EKEventStore eventsStore = new EKEventStore();
        public AlarmService()
        {
            ASkForPermissionsAsync();
        }

        public async Task<bool> CreateCalendarForAppAlarmsAsync()
        {
            bool hasPermission = await ASkForPermissionsAsync();

            if (hasPermission && appCalendar == null)
            {
                appCalendar = EKCalendar.Create(EKEntityType.Event, eventsStore);
                appCalendar.Title = "My App Calendar";
                appCalendar.Source = eventsStore.Sources.Where(s => s.SourceType == EKSourceType.CalDav).FirstOrDefault();
                appCalendar.CGColor = UIColor.Purple.CGColor;
                bool saved = eventsStore.SaveCalendar(appCalendar, true, out NSError e);
                return saved;
            }
            return true;
        }

        public async Task<bool> CheckIfAlarmAlreadyExistAsync(string id)
        {
            bool hasPermission = await ASkForPermissionsAsync();
            if (!hasPermission || appCalendar == null)
                return false;

            var calendarEvent = eventsStore.GetCalendarItem(id);
            if (calendarEvent == null)
                return false;

            return true;
        }

        public async Task<string> CreateAlarmAsync(string title, string description, DateTime timeInit, DateTime timeEnd, int alarmMinutes)
        {
            bool hasPermission = await ASkForPermissionsAsync();
            if (!hasPermission)
                return string.Empty;
            if (appCalendar == null)
                await CreateCalendarForAppAlarmsAsync();

            if (appCalendar != null)
            {
                EKEvent reminder = EKEvent.FromStore(eventsStore);
                reminder.AddAlarm(EKAlarm.FromTimeInterval(-(alarmMinutes * 60)));
                reminder.StartDate = timeInit.ToNSDate();
                reminder.EndDate = timeEnd.ToNSDate();
                reminder.Title = title;
                reminder.Notes = description;
                reminder.Calendar = appCalendar;
                bool saved = eventsStore.SaveEvent(reminder, EKSpan.ThisEvent, out NSError e);
                if (e == null)
                    return reminder.CalendarItemIdentifier;
            }

            return string.Empty;
        }

        private Task<bool> ASkForPermissionsAsync()
        {
            TaskCompletionSource<bool> tcs = new TaskCompletionSource<bool>();
            eventsStore.RequestAccess(EKEntityType.Event, (bool granted, NSError error) =>
            {
                if (!granted)
                {
                    var alert = UIAlertController.Create("Acceso denegado", "Acceso al calendario denegado por el usuario", UIAlertControllerStyle.Alert);
                    UIApplication.SharedApplication.KeyWindow.RootViewController.PresentViewController(alert, true, null);
                }
                else
                {
                    appCalendar = eventsStore.Calendars.Where(c => c.Title.ToLowerInvariant().Equals(calendarTitle.ToLowerInvariant())).FirstOrDefault();
                }
                tcs.SetResult(granted);
            });

            return tcs.Task;
        }

        public async Task<bool> DeleteAlarmAsync(string id)
        {
            bool hasPermission = await ASkForPermissionsAsync();
            if (!hasPermission || appCalendar == null)
                return false;

            var calendarEvent = eventsStore.GetCalendarItem(id);
            if (calendarEvent != null)
            {
                eventsStore.RemoveEvent((EKEvent)calendarEvent, EKSpan.ThisEvent, true, out NSError e);
                if (e == null)
                    return true;
            }

            return false;
        }
    }
}