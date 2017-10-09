using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using RateFeedNotificationService.Hubs;
using Microsoft.AspNetCore.SignalR.Client;
using System.Net.WebSockets;

namespace RateFeedNotificationService.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public async Task Post([FromBody]string value)
        {
            var connection = new HubConnectionBuilder()
                .WithUrl("http://localhost:54267/rates-feed-hub")
                .WithConsoleLogger()
                .Build();

            connection.On<RateFeedData>("Send", data =>
            {
                Console.WriteLine(data);
            });

            await connection.StartAsync();

            await connection.SendAsync("Send", new RateFeedData { RateValue = 0.705, BaseCurrency = "CAD", TargetCurrency = "USD" });

        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
            var hub = new RateFeedHub();
            hub.Clients = Startup.RateFeedHubContext.Clients;
            hub.Send(new RateFeedData { RateValue = 0.705, BaseCurrency = "CAD", TargetCurrency = "USD" });

        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
