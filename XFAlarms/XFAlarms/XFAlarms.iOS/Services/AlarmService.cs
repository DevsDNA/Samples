[assembly: Xamarin.Forms.Dependency(typeof(XFAlarms.iOS.Services.AlarmService))]
namespace XFAlarms.iOS.Services
{
    using CoreGraphics;
    using EventKit;
    using Foundation;
    using System;
    using System.Linq;
    using UIKit;
    using Xamarin.Forms;
    using Xamarin.Forms.Platform.iOS;
    using XFAlarms.Services;

    public class AlarmService : IAlarmService
    {
        private string calendarTitle = "My App Calendar";
        private static EKCalendar appCalendar;
        private static EKEventStore eventsStore = new EKEventStore();
        private static bool grantedPermission = false;
        public AlarmService()
        {
            eventsStore.RequestAccess(EKEntityType.Event, (bool granted, NSError error) =>
            {
                grantedPermission = granted;
                if (!grantedPermission)
                    new UIAlertView("Acceso denegado", "Acceso al calendario denegado", null, "ok", null);
                else
                    appCalendar = eventsStore.Calendars.Where(c => c.Title.ToLowerInvariant().Equals(calendarTitle.ToLowerInvariant())).FirstOrDefault();
            });
        }

        public bool CreateCalendarForAppAlarms()
        {
            if (appCalendar == null)
            {
                appCalendar = EKCalendar.Create(EKEntityType.Event, eventsStore);
                appCalendar.Title = "My App Calendar";
                appCalendar.Source = eventsStore.Sources.Where(s => s.SourceType == EKSourceType.Local).FirstOrDefault();
                appCalendar.CGColor = UIColor.Purple.CGColor;
                bool saved = eventsStore.SaveCalendar(appCalendar, true, out NSError e);
                return saved;
            }
            return true;
        }

        public bool CheckIfAlarmAlreadyExist()
        {
            throw new NotImplementedException();
        }

        public bool CreateAlarm(string title, string description, DateTime timeInit, DateTime timeEnd, int alarmMinutes, string id)
        {
            bool calendarCreated = false;
            if (appCalendar == null)
                calendarCreated = CreateCalendarForAppAlarms();
            if (calendarCreated)
            {
                EKEvent reminder = EKEvent.FromStore(eventsStore);
                reminder.AddAlarm(EKAlarm.FromTimeInterval(-(alarmMinutes * 60)));
                reminder.StartDate = timeInit.ToNSDate();
                reminder.EndDate = timeEnd.ToNSDate();
                reminder.Title = title;
                reminder.Notes = description;
                reminder.Calendar = appCalendar;
                bool saved = eventsStore.SaveEvent(reminder, EKSpan.ThisEvent, out NSError e);
                id = reminder.CalendarItemIdentifier;
                return saved;
            }

            return false;
        }

        public bool DeleteAlarm(int id)
        {
            return false;
        }
    }
}