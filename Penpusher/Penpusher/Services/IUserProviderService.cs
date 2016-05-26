using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Penpusher.Services
{
    public  interface IUserProviderService
    {
        bool SubscribeUserToProvider(int userId, int providerId, bool isSubscribe);
        IEnumerable<int> GetProvidersForUser(int userId);
    }
}