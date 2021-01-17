using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace WebMonitoringApi.Hubs
{
    public class NotificationHub : Hub
    {
        public NotificationHub()
        {
            //this.Context.ConnectionId;
        }

        //public async Task BroadcastChartData( data) => await Clients.All.SendAsync("broadcastchartdata", data);

        public async Task SendResponseStatus()
        {
            //await Clients.Client(Context.ConnectionId).SendAsync("ReceivedResponseStatus", null);
        }
        // => await Clients.Client(connectionId).SendAsync("NotificationReceived", data);
    }
}
