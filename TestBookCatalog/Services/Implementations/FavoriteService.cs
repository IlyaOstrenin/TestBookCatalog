using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestBookCatalog.Models;
using TestBookCatalog.Repositories.Interfaces;
using TestBookCatalog.Services.Interfaces;

namespace TestBookCatalog.Services.Implementations
{
    public class FavoriteService : IFavoriteService
    {
        private readonly IFavoriteRepository _favoriteRepository;
        private readonly IBookService _bookService;

        public FavoriteService(IFavoriteRepository favoriteRepository, IBookService bookService)
        {
            _favoriteRepository = favoriteRepository;
            _bookService = bookService;
        }

        public async Task<IEnumerable<Favorite>> GetListAsync(Guid userId)
        {
            return await _favoriteRepository.GetListAsync(userId);
        }

        public async Task SaveAsync(Guid bookId, Guid userId)
        {
            await _bookService.GetByIdAsync(bookId);

            if (await _favoriteRepository.IsExistAsync(bookId, userId))
                throw new InvalidOperationException("already exist");

            var favorite = new Favorite { BookId = bookId, UserId = userId };

            await _favoriteRepository.SaveAsync(favorite);
        }

        public async Task DeleteAsync(Guid bookId, Guid userId)
        {
            if (!await _favoriteRepository.IsExistAsync(bookId, userId))
                throw new ArgumentException("book not found");

            await _favoriteRepository.DeleteAsync(bookId, userId);
        }
    }
}
