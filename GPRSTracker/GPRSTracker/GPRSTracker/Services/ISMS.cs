namespace GPRSTracker.Services
{
    using GPRSTracker.Models;

    public interface ISMS
    {
        void Send(SMSMessage sMSMessage);
    }
}
