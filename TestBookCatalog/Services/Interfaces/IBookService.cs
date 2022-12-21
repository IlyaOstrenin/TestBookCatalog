using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestBookCatalog.Models;

namespace TestBookCatalog.Services.Interfaces
{
    public interface IBookService
    {
        Task<Book> GetByIdAsync(Guid bookId);
        Task<IEnumerable<Book>> SearchAsync(string phrase);
        Task<IEnumerable<Category>> GetCategoriesAsync();
        Task<Guid> SaveAsync(BookInput book);
        Task UpdateAsync(BookInput book);
        Task DeleteAsync(Guid bookId);
    }
}
