using ThirdPartyRateWebHook.API.Controllers;

namespace ThirdPartyRateWebHook.API.ServiceProvider
{
    public interface IThirdPartyRatesCommandRA
    {
        void Save(RateData rateData);
    }
}
