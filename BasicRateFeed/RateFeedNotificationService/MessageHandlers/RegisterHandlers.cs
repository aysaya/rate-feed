using Microsoft.Azure.ServiceBus;
using RateFeedNotificationService.Bus;

namespace RateFeedNotificationService.MessageHandlers
{
    public interface IRegisterHandlers
    {
        void Register();
    }
    public class RegisterHandlers : IRegisterHandlers
    {
        private readonly IHandleRateFeedSubscriptionMessage handler;
        private readonly IProvideRateFeedBusSettings settings;
        public RegisterHandlers(IHandleRateFeedSubscriptionMessage handler, IProvideRateFeedBusSettings settings)
        {
            this.handler = handler;
            this.settings = settings;
        }

        public void Register()
        {
            new SubscriptionClient(settings.ConnectionString, settings.TopicName, settings.SubscriptionName)
                .RegisterMessageHandler(handler.Handle, handler.HandleOption);
        }
    }
}
