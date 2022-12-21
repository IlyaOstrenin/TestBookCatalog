using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestBookCatalog.Models;
using TestBookCatalog.Repositories.Interfaces;
using TestBookCatalog.Services.Interfaces;

namespace TestBookCatalog.Services.Implementations
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IFavoriteRepository _favoriteRepository;
        private readonly IMediaFileRepository _mediaFileRepository;

        public BookService(IBookRepository bookRepository, IFavoriteRepository favoriteRepository, IMediaFileRepository mediaFileRepository)
        {
            _bookRepository = bookRepository;
            _favoriteRepository = favoriteRepository;
            _mediaFileRepository = mediaFileRepository;
        }

        public async Task DeleteAsync(Guid bookId)
        {
            if (await _bookRepository.IsExistAsync(bookId))
            {
                await _bookRepository.DeleteAsync(bookId);
                await _favoriteRepository.HidenAsync(bookId);
            }
        }

        public async Task<Book> GetByIdAsync(Guid bookId)
        {
            var book = await _bookRepository.GetByIdAsync(bookId);

            if (book == null)
                throw new ArgumentException("book not found");

            return book;
        }

        public async Task<IEnumerable<Category>> GetCategoriesAsync()
        {
            return await _bookRepository.GetCategoriesAsync();
        }

        public async Task<Guid> SaveAsync(BookInput bookInput)
        {
            var bookDb = new Book();

            await BuildBookDb(bookDb, bookInput);

            await _bookRepository.SaveAsync(bookDb);

            return bookDb.Id;
        }

        public Task<IEnumerable<Book>> SearchAsync(string phrase)
        {
            return _bookRepository.SearchAsync(phrase);
        }

        public async Task UpdateAsync(BookInput bookInput)
        {
            var bookDb = await GetByIdAsync(bookInput.Id);

            await _bookRepository.DeleteBookCategoriesAsync(bookDb.Categories);

            await BuildBookDb(bookDb, bookInput);

            await _bookRepository.UpdateAsync(bookDb);
        }

        private async Task BuildBookDb(Book bookDb, BookInput bookInput)
        {
            var categories = await _bookRepository.GetCategoriesAsync(bookInput.CategoriesIds);

            if (categories.Count == 0 || categories.Count != bookInput.CategoriesIds.Count())
                throw new ArgumentException("category not found");

            var cover = await _mediaFileRepository.GetByIdAsync(bookInput.CoverId.Value);

            if (cover == null)
                throw new ArgumentException("cover not found");

            bookDb.Author = bookInput.Author;
            bookDb.Year = bookInput.Year;
            bookDb.Title = bookInput.Title;
            bookDb.Description = bookInput.Description;
            bookDb.NumberOfPages = bookInput.NumberOfPages;
            bookDb.CoverId = cover.Id;
            bookDb.Categories = categories.Select(x => new BookCategory { BookId = bookDb.Id, CategoryId = x.Id }).ToList();
        }
    }
}
