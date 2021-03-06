﻿using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace RateFeedNotificationService.Hubs
{
    public class RateFeedHub : Hub
    {
        
        public Task Send(RateFeedData rateFeedData)
        {
            return Clients.All.InvokeAsync("Send", rateFeedData);
        }
    }
}
