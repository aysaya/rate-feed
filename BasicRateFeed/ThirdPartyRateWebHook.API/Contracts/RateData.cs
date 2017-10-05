using System.Runtime.Serialization;

namespace ThirdPartyRateWebHook.API.Controllers
{
    [DataContract]
    public class RateData
    {
        [DataMember(Name ="refId")]
        public string ReferenceId { get; set; }

        [DataMember(Name = "baseCurrency")]
        public string BaseCurrency { get; set; }

        [DataMember(Name = "targetCurrency")]
        public string TargetCurrency { get; set; }

        [DataMember(Name = "rateValue")]
        public double RateValue  { get; set; }

    }
}