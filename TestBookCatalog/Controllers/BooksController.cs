using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using TestBookCatalog.Models;
using TestBookCatalog.Services.Interfaces;

namespace TestBookCatalog.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }

        /// <summary>
        /// get a specific book
        /// </summary>
        /// <param name="id">bookId</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<Book> Get(Guid id) => await _bookService.GetByIdAsync(id);

        /// <summary>
        /// find a book by title, author, description and category
        /// </summary>
        /// <param name="phrase"></param>
        /// <returns></returns>
        [HttpGet("search/{phrase}")]
        public async Task<IEnumerable<Book>> Get([StringLength(20, MinimumLength = 3)] string phrase) => await _bookService.SearchAsync(phrase);

        /// <summary>
        /// get a list of all categories
        /// </summary>
        /// <returns></returns>
        [HttpGet("categories")]
        public async Task<IEnumerable<Category>> Get() => await _bookService.GetCategoriesAsync();

        /// <summary>
        /// add a new book
        /// </summary>
        /// <param name="book"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<Guid> Post(BookInput book) => await _bookService.SaveAsync(book);

        /// <summary>
        /// update the data of a specific book
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        [Authorize(Roles = "admin")]
        public async Task Put(BookInput model) => await _bookService.UpdateAsync(model);

        /// <summary>
        /// delete a specific book
        /// </summary>
        /// <param name="id">bookId</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public async Task Delete(Guid id) => await _bookService.DeleteAsync(id);
    }
}
