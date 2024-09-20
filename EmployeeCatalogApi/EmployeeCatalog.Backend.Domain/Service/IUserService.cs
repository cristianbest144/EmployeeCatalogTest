using EmployeeCatalog.Backend.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeCatalog.Backend.Domain.Service
{
    public interface IUserService
    {
        Task<User> GetUserByUsernameAndPasswordAsync(string username, string passwordHash);
        Task RegisterUserAsync(User user);
        Task<User> GetUserByUsernameAsync(string username);
        string CreatePasswordHash(string password);
    }
}
