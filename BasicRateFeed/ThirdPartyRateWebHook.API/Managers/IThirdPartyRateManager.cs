using ThirdPartyRateWebHook.API.Controllers;

namespace ThirdPartyRateWebHook.API.Managers
{
    public interface IThirdPartyRateManager
    {
        void Process(RateData rateData, string key);
    }
}