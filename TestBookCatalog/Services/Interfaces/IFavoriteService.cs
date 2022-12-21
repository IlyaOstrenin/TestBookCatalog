using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestBookCatalog.Models;

namespace TestBookCatalog.Services.Interfaces
{
    public interface IFavoriteService
    {
        Task<IEnumerable<Favorite>> GetListAsync(Guid userId);
        Task DeleteAsync(Guid bookId, Guid userId);
        Task SaveAsync(Guid bookId, Guid userId);
    }
}
