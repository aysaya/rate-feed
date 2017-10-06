using Microsoft.AspNetCore.Mvc;
using ThirdPartyRateWebHook.API.Managers;

namespace ThirdPartyRateWebHook.API.Controllers
{
    [Route("api/[controller]")]
    public class ThirdPartyRatesController : Controller
    {
        private readonly IThirdPartyRateManager thirdPartyRateManager;
        
        public ThirdPartyRatesController(IThirdPartyRateManager thirdPartyRateManager)
        {
            this.thirdPartyRateManager = thirdPartyRateManager;
        }

        [HttpPost]
        public void Post([FromBody]RateData value, [FromHeader]string key)
        {
            thirdPartyRateManager.Process(value, key);
        }
    }
}
