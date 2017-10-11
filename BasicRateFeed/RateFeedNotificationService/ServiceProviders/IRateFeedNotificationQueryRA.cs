using RateFeedNotificationService.Hubs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RateFeedNotificationService.ServiceProviders
{
    public interface IRateFeedNotificationQueryRA
    {
        Task<RateFeedData[]> GetAllAsync();
    }
}
