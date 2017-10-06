using Microsoft.Azure.ServiceBus;
using Newtonsoft.Json;

namespace ThirdPartyRateWebHook.API.MessageSenders
{
    public class AzureServiceBusMessageSender : IPublishThirdPartyRate
    {
        public void Publish<T>(string key, T message)
        {
            var connStr = $"Endpoint=sb://payments-servicebus-aysaya.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey={key}";
            var queueName = "third-party-rates";

            var client = new TopicClient(connStr, queueName);
            var msg = new Message(System.Text.Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message)));

            client.SendAsync(msg);
        }        
    }
}
