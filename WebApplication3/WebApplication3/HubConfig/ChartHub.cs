using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication3.Model;

namespace WebApplication3.HubConfig
{
    public class ChartHub:Hub
    {
        public async Task BroadcastChartData(List<ChartModel> data) =>
            await Clients.All.SendAsync("broadcastchartdata", data);
    }
}
