using System;
using System.Collections.Generic;
using System.Linq;

namespace Penpusher.Services
{
    public class UserProvidersService : IUserProviderService
    {
        private readonly IRepository<UsersNewsProvider> repository;

        public UserProvidersService(IRepository<UsersNewsProvider> repository)
        {
            this.repository = repository;
        }

        public bool SubscribeUserToProvider( int providerId, bool isSubscribe)
        {
            var userId = 5;
            try
            {
                if (isSubscribe)
                {
                    repository.Edit(new UsersNewsProvider
                    {
                        IdNewsProvider = providerId,
                        IdUser = userId
                    });
                }
                else
                {
                    UsersNewsProvider userProvider =
                        repository.GetAll().First(up => up.IdUser == userId && up.IdNewsProvider == providerId);
                    if (userProvider != null)
                    {
                        repository.Delete(userProvider.Id);
                    }
                }
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool IsUserSubscribedOnProvider(int providerId)
        {
            //TODO: Use  HttpContext.Current.User id

            if (repository.GetAll().Count(up => up.IdNewsProvider == providerId && up.IdUser == 5) > 0)
                return true;
            return false;
        }

        public IEnumerable<int> GetProvidersForUser()
        {
            //TODO: USe  HttpContext.Current.User id
            return repository.GetAll().Where(np => np.IdUser == 5).Select(np => np.Id);
        }
    }
}