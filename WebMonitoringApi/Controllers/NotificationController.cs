using WebMonitoringApi.Data;
using WebMonitoringApi.Hubs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace WebMonitoringApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController
    {
        private readonly IHubContext<NotificationHub> _hub;
        private readonly ApplicationDbContext _dbContext;

        public NotificationController(IHubContext<NotificationHub> hub, ApplicationDbContext dbContext)
        {
            _hub = hub;
            _dbContext = dbContext;
        }

        [HttpPost]
        public IActionResult Create()
        {
            /*var timerManager = new TimerManager(
                () => _hub.Clients.All.SendAsync("transferchartdata", DataManager.GetData()));

            return Ok(new { Message = "Request Completed" });*/

            return null;
        }
    }
}
