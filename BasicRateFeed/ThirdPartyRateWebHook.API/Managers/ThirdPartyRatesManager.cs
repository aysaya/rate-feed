using System;
using ThirdPartyRateWebHook.API.Controllers;
using ThirdPartyRateWebHook.API.MessageSenders;
using ThirdPartyRateWebHook.API.MessageSenders.Messages;
using ThirdPartyRateWebHook.API.ServiceProvider;

namespace ThirdPartyRateWebHook.API.Managers
{
    public class ThirdPartyRatesManager : IThirdPartyRateManager
    {
        private readonly IThirdPartyRatesCommandRA thirdPartyRatesCommandRA;
        private readonly IPublishThirdPartyRate publishThirdPartyRate;

        public ThirdPartyRatesManager(IThirdPartyRatesCommandRA thirdPartyRatesCommandRA, IPublishThirdPartyRate publishThirdPartyRate)
        {
            this.publishThirdPartyRate = publishThirdPartyRate;
            this.thirdPartyRatesCommandRA = thirdPartyRatesCommandRA;
        }

        public void Process(RateData rateData, string key)
        {
            if (rateData == null || string.IsNullOrEmpty(key))
                return;

            Save(rateData);
            Publish(key, rateData);
        }

        private void Save(RateData rateData)
        {
            thirdPartyRatesCommandRA.Save(rateData);
        }

        private void Publish(string key, RateData rateData)
        {
            publishThirdPartyRate.Publish(key,
                new ThirdPartyRateReceived
                {
                    Id = Guid.NewGuid(),
                    Rate = rateData.RateValue,
                    SettlementCurrency = rateData.BaseCurrency,
                    TradeCurrency = rateData.TargetCurrency
                });
        }

    }
}
