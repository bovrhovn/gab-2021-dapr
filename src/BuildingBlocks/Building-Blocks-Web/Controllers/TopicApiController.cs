using System;
using System.Threading.Tasks;
using Building_Blocks_Models;
using Building_Blocks_Web.Hubs;
using Dapr;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace Building_Blocks_Web.Controllers
{
    [ApiController]
    public class TopicApiController : ControllerBase
    {
        private readonly ILogger<TopicApiController> logger;
        private readonly IHubContext<AlertHub> hubContext;

        public TopicApiController(ILogger<TopicApiController> logger, IHubContext<AlertHub> hubContext)
        {
            this.logger = logger;
            this.hubContext = hubContext;
        }

        [Topic("servicebus-pubsub","messages")]
        [HttpPost("messages")]
        public async Task<IActionResult> Handle([FromBody]AmmountMessage message)
        {
            string info = $"Processing event with id {message.Id} with ammount {message.Ammount}";
            logger.LogInformation(info);
            //sending data to signalr
            await hubContext.Clients.All.SendAsync("alertMessage", info);
            
            logger.LogInformation($"Info has been sent at {DateTime.Now}");
            return Ok();
        }
    }
}