using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;
using ExpenseTracker.Models;
using ExpenseTracker.Data;
using BCrypt.Net;

namespace ExpenseTracker.Services
{
    public class AuthService
    {
        private readonly AppDbContext _context;

        public AuthService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<User> Register(string name, string email, string password)
        {
            var userExists = await _context.Users.AnyAsync(u => u.Email == email);
            if (userExists) return null;

            var hashed = BCrypt.Net.BCrypt.HashPassword(password);

            var user = new User
            {
                Username = name,
                Email = email,
                PasswordHash = hashed
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User> Login(string email, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);

            if (user == null) return null;

            if (!BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
            {
                return null;
            }
            return user;
        }
    }
}