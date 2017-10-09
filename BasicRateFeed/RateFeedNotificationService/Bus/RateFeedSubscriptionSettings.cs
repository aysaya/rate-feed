namespace RateFeedNotificationService.Bus
{
    public interface IProvideRateFeedBusSettings
    {
        string ConnectionString { get; }
        string TopicName { get; }
        string SubscriptionName { get; }
    }
    public class RateFeedSubscriptionSettings : IProvideRateFeedBusSettings
    {
        private readonly string connectionString;
        private readonly string topicName;
        private readonly string subscriptionName;
        public RateFeedSubscriptionSettings(string key)
        {
            connectionString = $"Endpoint=sb://payments-servicebus-aysaya.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey={key}";
            subscriptionName = "rate-feed-notification-service";
            topicName = "third-party-rates";
        }

        string IProvideRateFeedBusSettings.ConnectionString => connectionString;

        string IProvideRateFeedBusSettings.TopicName => topicName;

        string IProvideRateFeedBusSettings.SubscriptionName => subscriptionName;
        
    }
}
