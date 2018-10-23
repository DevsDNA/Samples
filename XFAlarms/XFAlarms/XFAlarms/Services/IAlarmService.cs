namespace XFAlarms.Services
{
    using System;

    public interface IAlarmService
    {
        bool CheckIfAlarmAlreadyExist();

        bool CreateAlarm(string title, string description, DateTime time, int id);

        bool DeleteAlarm(int id);
    }
}
