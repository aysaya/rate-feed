using System.Collections.Generic;
using System.Linq;
using RateFeedNotificationService.Hubs;
using System.Threading.Tasks;

namespace RateFeedNotificationService.ServiceProviders
{
    public class MemoryPersistence : IRateFeedNotificationCommandRA, IRateFeedNotificationQueryRA
    {
        private static HashSet<RateFeedData> cache = new HashSet<RateFeedData>();

        public async Task<RateFeedData[]> GetAllAsync()
        {
            return await Task.FromResult(cache.ToArray());            
        }
        
        
        public async Task SaveAsync(RateFeedData rateFeedData)
        {
            if (rateFeedData != null)
            {
                cache.Add(rateFeedData);
            }
            await Task.CompletedTask;
        }

      
    }
}
