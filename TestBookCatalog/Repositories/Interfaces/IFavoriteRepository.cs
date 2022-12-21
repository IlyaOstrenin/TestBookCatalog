using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestBookCatalog.Models;

namespace TestBookCatalog.Repositories.Interfaces
{
    public interface IFavoriteRepository : IRepositoryBase<Favorite>
    {
        Task<IEnumerable<Favorite>> GetListAsync(Guid userId);
        Task DeleteAsync(Guid bookId, Guid userId);
        Task HidenAsync(Guid bookId);
        Task<bool> IsExistAsync(Guid bookId, Guid userId);
    }
}
