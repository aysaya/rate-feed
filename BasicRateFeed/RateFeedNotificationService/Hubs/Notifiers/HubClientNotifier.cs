using RateFeedNotificationService.MessageHandlers.Messages;

namespace RateFeedNotificationService.Hubs.Notifiers
{
    public interface INotifyHubClient
    {
        void Notify(ThirdPartyRateReceived payload);
    }

    public class HubClientNotifier : INotifyHubClient
    {
        public void Notify(ThirdPartyRateReceived payload)
        {
            var hub = new RateFeedHub();
            hub.Clients = Startup.RateFeedHubContext.Clients;
            hub.Send(new RateFeedData { RateValue = payload.Rate, BaseCurrency = payload.SettlementCurrency, TargetCurrency = payload.TradeCurrency });
        }
    }
}
