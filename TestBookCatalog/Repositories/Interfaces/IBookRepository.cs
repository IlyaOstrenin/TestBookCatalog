using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestBookCatalog.Models;

namespace TestBookCatalog.Repositories.Interfaces
{
    public interface IBookRepository : IRepositoryBase<Book>
    {
        Task<Book> GetByIdAsync(Guid bookId);
        Task<IEnumerable<Book>> SearchAsync(string phrase);
        Task<IEnumerable<Category>> GetCategoriesAsync();
        Task DeleteBookCategoriesAsync(List<BookCategory> bookCategories);
        Task<List<Category>> GetCategoriesAsync(int[] categoriesIds);
        Task UpdateAsync(Book book);
        Task<bool> IsExistAsync(Guid bookId);
        Task DeleteAsync(Guid bookId);
    }
}
