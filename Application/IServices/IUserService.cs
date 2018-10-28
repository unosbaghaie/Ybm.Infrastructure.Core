using ProductDomainModels.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.IServices
{
    public interface IUserService 
    {
        Task<User> GetUserAsync(int id);
        Task<User> CreateUser(User user);
    }
}
