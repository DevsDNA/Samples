namespace AppSignalR.Services
{
    using Microsoft.AspNetCore.SignalR.Client;
    using System;

    public class SignalRretryPolicy : IRetryPolicy
    {
        public TimeSpan? NextRetryDelay(RetryContext retryContext)
        {
            return TimeSpan.FromSeconds(10);
        }
    }
}
