using EmployeeCatalog.Backend.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeCatalog.Backend.Data.Repository
{
    public interface IUserRepository
    {
        Task<User> GetUserByUsernameAndPasswordAsync(string username, string passwordHash);
        Task<User> GetUserByUsernameAsync(string username);
        Task RegisterUserAsync(User user);

        string CreatePasswordHash(string password);
    }
}
