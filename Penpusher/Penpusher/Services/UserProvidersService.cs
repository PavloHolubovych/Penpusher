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

        public bool SubscribeUserToProvider(int providerId, bool isSubscribe)
        {
            try
            {
                if (isSubscribe)
                {
                    repository.Edit(new UsersNewsProvider
                    {
                        IdNewsProvider = providerId,
                        IdUser = Constants.UserId
                    });
                }
                else
                {
                    UsersNewsProvider userProvider =
                        repository.GetAll().First(up => up.IdUser == Constants.UserId && up.IdNewsProvider == providerId);
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
            if (repository.GetAll().Count(up => up.IdNewsProvider == providerId && up.IdUser == Constants.UserId) > 0)
            {
                return true;
            }
            return false;
        }

        public IEnumerable<int> GetProvidersForUser()
        {
            return repository.GetAll().Where(np => np.IdUser == Constants.UserId).Select(np => np.Id);
        }
    }
}