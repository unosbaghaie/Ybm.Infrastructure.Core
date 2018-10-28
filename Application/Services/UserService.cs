using Application.IServices;
using ProductDomainModels.IRepositories;
using ProductDomainModels.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class UserService : IUserService
    {
        readonly IUserRepository userRepository;
        public UserService(IUserRepository _userRepository)
        {
            userRepository = _userRepository;
        }



        public async Task<User> GetUserAsync(int id)
        {
            return await userRepository.FirstOrDefaultAsync(q=> q.Id == id);
        }

        public async Task<User> CreateUser(User user)
        {
             return await userRepository.CreateAsync(user);
        }
    }
}
