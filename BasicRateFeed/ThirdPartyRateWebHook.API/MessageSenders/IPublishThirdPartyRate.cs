namespace ThirdPartyRateWebHook.API.MessageSenders
{
    public interface IPublishThirdPartyRate
    {
        void Publish<T>(T message);
    }
}
