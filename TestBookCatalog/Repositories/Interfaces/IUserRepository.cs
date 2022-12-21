using System;
using System.Threading.Tasks;
using TestBookCatalog.Models;

namespace TestBookCatalog.Repositories.Interfaces
{
    public interface IUserRepository : IRepositoryBase<User>
    {
        Task<User> GetAsync(string phoneNumber);
        Task<User> GetAsync(Guid userId);
        Task<Role> GetRoleAsync(string name);
        Task<bool> IsExistAsync(string phoneNumber);
    }
}
