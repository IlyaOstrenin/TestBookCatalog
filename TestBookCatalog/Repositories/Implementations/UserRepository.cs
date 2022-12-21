using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using TestBookCatalog.Models;
using TestBookCatalog.Repositories.Interfaces;

namespace TestBookCatalog.Repositories.Implementations
{
    public class UserRepository : IUserRepository
    {
        public async Task<User> GetAsync(string phoneNumber)
        {
            using (var _context = new TestBookCatalogDbContext())
            {
                return await _context.Users
                    .Where(x => x.PhoneNumber.Equals(phoneNumber))
                    .Include(x => x.Role)
                    .FirstOrDefaultAsync();
            }
        }

        public async Task<User> GetAsync(Guid userId)
        {
            using (var _context = new TestBookCatalogDbContext())
            {
                return await _context.Users
                    .Where(x => x.Id.Equals(userId))
                    .Include(x => x.Role)
                    .FirstOrDefaultAsync();
            }
        }

        public async Task<Role> GetRoleAsync(string name)
        {
            using (var _context = new TestBookCatalogDbContext())
            {
                return await _context.Roles.FirstOrDefaultAsync(x => x.Name.Equals(name));
            }
        }

        public async Task<bool> IsExistAsync(string phoneNumber)
        {
            using (var _context = new TestBookCatalogDbContext())
            {
                return await _context.Users.AnyAsync(x => x.PhoneNumber.Equals(phoneNumber));
            }
        }

        public async Task SaveAsync(User user)
        {
            using (var _context = new TestBookCatalogDbContext())
            {
                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();
            }
        }
    }
}
