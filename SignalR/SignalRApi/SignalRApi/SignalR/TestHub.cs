using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace SignalRApi.SignalR
{
    public class TestHub : Hub
    {
        private static Dictionary<int, string> deviceConnections;
        private static Dictionary<string, int> connectionDevices;

        public TestHub()
        {
            deviceConnections = deviceConnections ?? new Dictionary<int, string>();
            connectionDevices = connectionDevices ?? new Dictionary<string, int>();
        }

        public override Task OnConnectedAsync()
        {
            Debug.WriteLine("SignalR server connected");
            return base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            int? deviceId = connectionDevices.ContainsKey(Context.ConnectionId) ?
                            (int?)connectionDevices[Context.ConnectionId] :
                            null;

            if (deviceId.HasValue)
            {
                deviceConnections.Remove(deviceId.Value);
                connectionDevices.Remove(Context.ConnectionId);
            }

            Debug.WriteLine($"SignalR server disconnected. Device: {deviceId}.");
            await base.OnDisconnectedAsync(exception);
        }

        [HubMethodName("Init")]
        public Task Init(DeviceInfo info)
        {
            deviceConnections.AddOrUpdate(info.Id, Context.ConnectionId);
            connectionDevices.AddOrUpdate(Context.ConnectionId, info.Id);

            return Task.CompletedTask;
        }

        [HubMethodName("SendMessageToAll")]
        public async Task SendMessageToAll(MessageItem item)
        {
            await Clients.All.SendAsync("NewMessage", item);
        }

        [HubMethodName("SendMessageToDevice")]
        public async Task SendMessageToDevice(MessageItem item)
        {
            Debug.WriteLine($"SignalR server send message {item.Message} from {item.SourceId} to  {item.TargetId}");

            if (deviceConnections.ContainsKey(item.TargetId))
                await Clients.Client(deviceConnections[item.TargetId]).SendAsync("NewMessage", item);
        }
    }
}
