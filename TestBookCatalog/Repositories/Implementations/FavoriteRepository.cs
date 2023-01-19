using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestBookCatalog.Models;
using TestBookCatalog.Repositories.Interfaces;

namespace TestBookCatalog.Repositories.Implementations
{
    public class FavoriteRepository : IFavoriteRepository
    {
        private readonly TestBookCatalogDbContext _context;

        public FavoriteRepository(TestBookCatalogDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Favorite>> GetListAsync(Guid userId)
        {
            return await _context.Favourites
                .Where(x => x.UserId.Equals(userId))
                .OrderByDescending(x => x.Created)
                .Include(x => x.Book.Cover)
                .Include(x => x.Book.Categories)
                .ThenInclude(x => x.Category)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<bool> IsExistAsync(Guid bookId, Guid userId)
        {
            return await _context.Favourites.AnyAsync(x => x.BookId.Equals(bookId) && x.UserId.Equals(userId));
        }

        public async Task SaveAsync(Favorite entity)
        {
            await _context.Favourites.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid bookId, Guid userId)
        {
            var favourite = await _context.Favourites.FirstOrDefaultAsync(x => x.BookId.Equals(bookId) && x.UserId.Equals(userId));
            _context.Favourites.RemoveRange(favourite);
            await _context.SaveChangesAsync();
        }

        public async Task HidenAsync(Guid bookId)
        {
            var entities = _context.Favourites.Where(x => x.BookId.Equals(bookId));
            await entities.ForEachAsync(x => x.IsHidden = true);
            await _context.SaveChangesAsync();
        }
    }
}
