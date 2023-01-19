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
        private readonly TestBookCatalogDbContext _context;

        public UserRepository(TestBookCatalogDbContext context)
        {
            _context = context;
        }

        public async Task<User> GetAsync(string phoneNumber)
        {
            return await _context.Users
                .Where(x => x.PhoneNumber.Equals(phoneNumber))
                .Include(x => x.Role)
                .FirstOrDefaultAsync();
        }

        public async Task<User> GetAsync(Guid userId)
        {
            return await _context.Users
                .Where(x => x.Id.Equals(userId))
                .Include(x => x.Role)
                .FirstOrDefaultAsync();
        }

        public async Task<Role> GetRoleAsync(string name)
        {
            return await _context.Roles.FirstOrDefaultAsync(x => x.Name.Equals(name));
        }

        public async Task<bool> IsExistAsync(string phoneNumber)
        {
            return await _context.Users.AnyAsync(x => x.PhoneNumber.Equals(phoneNumber));
        }

        public async Task SaveAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }
    }
}
