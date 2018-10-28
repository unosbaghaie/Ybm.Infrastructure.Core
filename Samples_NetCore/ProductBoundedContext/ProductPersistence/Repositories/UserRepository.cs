using ProductDomainModels.IRepositories;
using ProductDomainModels.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Ybm.Infrastructure.Core.Service;

namespace ProductPersistence.Repositories
{
    public class UserRepository : Service<User>, IUserRepository
    {
        public UserRepository(YbminfrastructurecoredbContext context)
            : base(context)
        {
            
        }
    }
}
