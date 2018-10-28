using ProductDomainModels.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Ybm.Infrastructure.Core.Interface;

namespace ProductDomainModels.IRepositories
{
    public interface IUserRepository : IService<User>
    {
    }
}
