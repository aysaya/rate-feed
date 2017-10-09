using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus;
using Newtonsoft.Json;
using System.Text;
using RateFeedNotificationService.MessageHandlers.Messages;
using RateFeedNotificationService.Hubs.Notifiers;

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
        public RateFeedSubscriptionHandler(INotifyHubClient hubClientNotifier)
        {
            this.hubClientNotifier = hubClientNotifier;
        }

        public Task Handle(Message message, CancellationToken token)
        {
            var payload = JsonConvert.DeserializeObject<ThirdPartyRateReceived>(Encoding.UTF8.GetString(message.Body));
            hubClientNotifier.Notify(payload);
            return Task.CompletedTask;
        }

        public Task HandleOption(ExceptionReceivedEventArgs arg)
        {
            Console.WriteLine($"Message handler encountered an exception {arg.Exception}.");
            return Task.CompletedTask;
        }
        
    }
}
