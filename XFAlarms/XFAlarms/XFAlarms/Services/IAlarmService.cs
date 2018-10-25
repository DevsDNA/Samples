namespace XFAlarms.Services
{
    using System;
    using System.Threading.Tasks;

    public interface IAlarmService
    {
        Task<bool> CreateCalendarForAppAlarmsAsync();

        Task<bool> CheckIfAlarmAlreadyExistAsync(string id);

        Task<string> CreateAlarmAsync(string title, string description, DateTime timeInit, DateTime timeEnd, int alarmMinutes);

        Task<bool> DeleteAlarmAsync(string id);
    }
}
