﻿namespace RateFeedNotificationService.Hubs
{
    public class RateFeedData
    {
        public string BaseCurrency { get; set; }
        public string TargetCurrency { get; set; }
        public double RateValue { get; set; }
        public string Reference { get; set; }
    }
}