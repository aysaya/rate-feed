using System.Collections.Generic;
using ThirdPartyRateWebHook.API.Controllers;

namespace ThirdPartyRateWebHook.API.ServiceProvider
{
    public class MemoryPeristence : IThirdPartyRatesCommandRA
    {
        private Dictionary<string, RateData> cache;

        public MemoryPeristence()
        {
            this.cache = new Dictionary<string, RateData>();
        }
        public void Save(RateData rateData)
        {
            if (rateData != null && !string.IsNullOrEmpty(rateData.ReferenceId))
            {
                cache[rateData.ReferenceId] = rateData;
            }
        }
    }
}
