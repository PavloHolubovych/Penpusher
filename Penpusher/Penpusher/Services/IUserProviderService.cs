using System.Collections.Generic;

namespace Penpusher.Services
{
    public interface IUserProviderService
    {
        bool SubscribeUserToProvider(int providerId, bool isSubscribe);

        IEnumerable<int> GetProvidersForUser();

        bool IsUserSubscribedOnProvider(int providerId);
    }
}