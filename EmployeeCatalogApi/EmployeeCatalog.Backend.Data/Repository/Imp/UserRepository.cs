using BCrypt.Net;
using EmployeeCatalog.Backend.Data.DAL;
using EmployeeCatalog.Backend.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeCatalog.Backend.Data.Repository.Imp
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<User> GetUserByUsernameAndPasswordAsync(string username, string passwordHash)
        {
            return await _context.Users
                .FromSqlRaw("EXEC LoginUser @Username = {0}, @PasswordHash = {1}", username, passwordHash)
                .FirstOrDefaultAsync();
        }

        public async Task RegisterUserAsync(User user)
        {
            await _context.Database.ExecuteSqlRawAsync("EXEC RegisterUser @Username = {0}, @PasswordHash = {1}, @Role = {2}",
                user.UserName, user.PasswordHash, user.Role);
        }

        public async Task<User> GetUserByUsernameAsync(string username)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.UserName == username);
        }

        public string CreatePasswordHash(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password, 12);
        }
    }
}
