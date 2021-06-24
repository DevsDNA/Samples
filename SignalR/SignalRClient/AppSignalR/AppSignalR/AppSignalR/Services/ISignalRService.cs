using System;
using System.Threading.Tasks;

namespace AppSignalR.Services
{
    public interface ISignalRService
    {
        event EventHandler<MessageItem> MessageReceived;
        event EventHandler Connecting;
        event EventHandler Connected;

        void StartWithReconnectionAsync();
        Task SendMessageToAll(MessageItem item);
        Task SendMessageToDevice(MessageItem item);
        Task StopAsync();
    }
}
