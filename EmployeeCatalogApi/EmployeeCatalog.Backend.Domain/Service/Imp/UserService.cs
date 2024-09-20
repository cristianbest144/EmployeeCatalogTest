using EmployeeCatalog.Backend.Data.Models;
using EmployeeCatalog.Backend.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeCatalog.Backend.Domain.Service.Imp
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;

        public UserService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task<User> GetUserByUsernameAndPasswordAsync(string username, string passwordHash)
        {
            return await userRepository.GetUserByUsernameAndPasswordAsync(username, passwordHash);
        }

        public async Task<User> GetUserByUsernameAsync(string username)
        {
            return await userRepository.GetUserByUsernameAsync(username);
        }

        public async Task RegisterUserAsync(User user)
        {
            await userRepository.RegisterUserAsync(user);
        }

        public string CreatePasswordHash(string password)
        {
            return userRepository.CreatePasswordHash(password);
        }
    }
}
