using Microsoft.AspNetCore.Mvc;

namespace ThirdPartyRateWebHook.API.Controllers
{
    [Route("api/[controller]")]
    public class ThirdPartyRatesController : Controller
    {
        [HttpPost]
        public void Post([FromBody]RateData value)
        {

        }

    }
}
