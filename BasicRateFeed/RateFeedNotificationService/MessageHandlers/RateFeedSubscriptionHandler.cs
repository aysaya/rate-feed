using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus;
using Newtonsoft.Json;
using System.Text;
using RateFeedNotificationService.MessageHandlers.Messages;
using RateFeedNotificationService.Hubs.Notifiers;
using RateFeedNotificationService.ServiceProviders;

namespace RateFeedNotificationService.MessageHandlers
{
    public interface IHandleRateFeedSubscriptionMessage
    {
        Task Handle(Message message, CancellationToken token);
        Task HandleOption(ExceptionReceivedEventArgs arg);
    }

    public class RateFeedSubscriptionHandler : IHandleRateFeedSubscriptionMessage
    {
        private readonly INotifyHubClient hubClientNotifier;
        private readonly IRateFeedNotificationCommandRA commandRA;

        public RateFeedSubscriptionHandler(INotifyHubClient hubClientNotifier, IRateFeedNotificationCommandRA commandRA)
        {
            this.hubClientNotifier = hubClientNotifier;
            this.commandRA = commandRA;
        }

        public async Task Handle(Message message, CancellationToken token)
        {
            var payload = JsonConvert.DeserializeObject<ThirdPartyRateReceived>(Encoding.UTF8.GetString(message.Body));
            hubClientNotifier.Notify(payload);
            await commandRA.SaveAsync(new Hubs.RateFeedData { BaseCurrency = payload.SettlementCurrency, TargetCurrency = payload.TradeCurrency, RateValue = payload.Rate, Reference = payload.Id.ToString()});
            //return Task.CompletedTask;
        }

        public Task HandleOption(ExceptionReceivedEventArgs arg)
        {
            Console.WriteLine($"Message handler encountered an exception {arg.Exception}.");
            return Task.CompletedTask;
        }
        
    }
}
