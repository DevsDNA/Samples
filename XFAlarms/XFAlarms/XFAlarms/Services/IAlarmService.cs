namespace XFAlarms.Services
{
    using System;

    public interface IAlarmService
    {
        bool CreateCalendarForAppAlarms();

        bool CheckIfAlarmAlreadyExist();

        bool CreateAlarm(string title, string description, DateTime timeInit, DateTime timeEnd, int alarmMinutes, string id);

        bool DeleteAlarm(int id);
    }
}
