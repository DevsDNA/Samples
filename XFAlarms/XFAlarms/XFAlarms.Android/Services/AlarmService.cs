namespace XFAlarms.Droid.Services
{
    using System;
    using XFAlarms.Services;

    public class AlarmService : IAlarmService
    {
        public bool CheckIfAlarmAlreadyExist()
        {
            throw new NotImplementedException();
        }

        public bool CreateAlarm(string title, string description, DateTime timeInit, DateTime timeEnd, int alarmMinutes, string id)
        {
            throw new NotImplementedException();
        }

        public bool CreateCalendarForAppAlarms()
        {
            throw new NotImplementedException();
        }

        public bool DeleteAlarm(int id)
        {
            throw new NotImplementedException();
        }
    }
}