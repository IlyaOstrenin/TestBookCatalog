using System;
using System.Threading.Tasks;
using TestBookCatalog.Models;

namespace TestBookCatalog.Services.Interfaces
{
    public interface IUserService
    {
        Task<User> GetAsync(string phoneNumber);
        Task<User> GetAsync(Guid userId);
        Task<string> SaveAsync(Login model);
    }
}
