namespace ThirdPartyRateWebHook.API.MessageSenders
{
    public interface IPublishThirdPartyRate
    {
        //key is just a way to hide the shared access key for the meantime
        void Publish<T>(string key, T message);
    }
}
