using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace Penpusher.Services
{
    public class UserProvidersService: IUserProviderService
    {
        private IRepository<UsersNewsProvider> repository; 

        public UserProvidersService(IRepository<UsersNewsProvider> repository)
        { 
            this.repository = repository;
        }

        public bool SubscribeUserToProvider(int userId, int providerId, bool isSubscribe)
        {
            try
            {
                if (isSubscribe)
                {
                    repository.Add(new UsersNewsProvider
                    {
                        IdNewsProvider = providerId,
                        IdUser = userId

                    });
                }
                else
                {
                    var userProvider =
                        repository.GetAll().First(up => up.IdUser == userId && up.IdNewsProvider == providerId);
                    if (userProvider != null)
                        repository.Delete(userProvider.Id);
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
            return repository.GetAll().Where(np => np.IdUser ==5).Select(np=>np.Id);
        }

        
    }
}