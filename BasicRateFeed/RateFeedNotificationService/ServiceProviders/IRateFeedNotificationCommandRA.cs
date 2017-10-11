using RateFeedNotificationService.Hubs;
using System.Threading.Tasks;

namespace RateFeedNotificationService.ServiceProviders
{
    public interface IRateFeedNotificationCommandRA
    {
        Task SaveAsync(RateFeedData rateFeedData);
    }
}
