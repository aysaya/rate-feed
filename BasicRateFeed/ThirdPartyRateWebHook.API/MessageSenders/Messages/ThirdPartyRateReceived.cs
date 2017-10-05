using Microsoft.Azure.ServiceBus;
using System;
using System.Runtime.Serialization;

namespace ThirdPartyRateWebHook.API.MessageSenders.Messages
{
    [DataContract]
    public class ThirdPartyRateReceived : Message
    {
        [DataMember]
        public Guid Id { get; set; }
        [DataMember]
        public double Rate { get; set; }
        [DataMember]
        public string TradeCurrency { get; set; }
        [DataMember]
        public string SettlementCurrency { get; set; }
    }
}
