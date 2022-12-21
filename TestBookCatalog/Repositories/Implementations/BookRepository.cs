using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestBookCatalog.Models;
using TestBookCatalog.Repositories.Interfaces;

namespace TestBookCatalog.Repositories.Implementations
{
    public class BookRepository : IBookRepository
    {
        public async Task DeleteAsync(Guid bookId)
        {
            using (var _context = new TestBookCatalogDbContext())
            {
                var book = await _context.Books.FirstOrDefaultAsync(x => x.Id.Equals(bookId));
                book.IsDeleted = true;
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteBookCategoriesAsync(List<BookCategory> bookCategories)
        {
            using (var _context = new TestBookCatalogDbContext())
            {
                _context.BooksCategories.RemoveRange(bookCategories);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Book> GetByIdAsync(Guid bookId)
        {
            using (var _context = new TestBookCatalogDbContext())
            {
                return await _context.Books
                    .Where(x => x.Id.Equals(bookId) && !x.IsDeleted)
                    .Include(x => x.Cover)
                    .Include(x => x.Categories)
                    .ThenInclude(x => x.Category)
                    .FirstOrDefaultAsync();
            }
        }

        public async Task<List<Category>> GetCategoriesAsync(int[] categoriesIds)
        {
            using (var _context = new TestBookCatalogDbContext())
            {
                return await _context.Categories
                    .Where(x => categoriesIds.Contains(x.Id))
                    .AsNoTracking()
                    .ToListAsync();
            }
        }

        public async Task<IEnumerable<Category>> GetCategoriesAsync()
        {
            using (var _context = new TestBookCatalogDbContext())
            {
                return await _context.Categories.ToListAsync();
            }
        }

        public async Task<bool> IsExistAsync(Guid bookId)
        {
            using (var _context = new TestBookCatalogDbContext())
            {
                return await _context.Books.AnyAsync(x => x.Id.Equals(bookId));
            }
        }

        public async Task SaveAsync(Book book)
        {
            using (var _context = new TestBookCatalogDbContext())
            {
                await _context.Books.AddAsync(book);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Book>> SearchAsync(string phrase)
        {
            using (var _context = new TestBookCatalogDbContext())
            {
                var books = _context.Books
                    .Where(x => !x.IsDeleted)
                    .Include(x => x.Categories)
                    .ThenInclude(x => x.Category)
                    .AsQueryable();

                books = books.Where(x => x.Author.ToLower().Contains(phrase) || 
                                    x.Title.ToLower().Contains(phrase) || 
                                    x.Description.ToLower().Contains(phrase) || 
                                    x.Categories.Any(z => z.Category.Name.ToLower().Contains(phrase)));

                return await books
                    .Include(x => x.Cover)
                    .AsNoTracking()
                    .ToListAsync();
            }
        }

        public async Task UpdateAsync(Book book)
        {
            using (var _context = new TestBookCatalogDbContext())
            {
                _context.Books.Update(book);
                await _context.SaveChangesAsync();
            }
        }
    }
}
