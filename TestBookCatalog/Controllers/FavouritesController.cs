using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestBookCatalog.Models;
using TestBookCatalog.Services.Interfaces;

namespace TestBookCatalog.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class FavouritesController : TestBookCatalogController
    {
        private readonly IFavoriteService _favoriteService;

        public FavouritesController(IHttpContextAccessor context, IFavoriteService favoriteService)
        {
            _context = context;
            _favoriteService = favoriteService;
        }

        /// <summary>
        /// get your favorite books
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IEnumerable<Favorite>> Get() => await _favoriteService.GetListAsync(UserId);

        /// <summary>
        /// add a book to favorites
        /// </summary>
        /// <param name="model">input model</param>
        /// <returns></returns>
        [HttpPost]
        public async Task Post(FavoriteInput model) => await _favoriteService.SaveAsync(model.BookId, UserId);

        /// <summary>
        /// delete a book from favorites
        /// </summary>
        /// <param name="id">bookId</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task Delete(Guid id) => await _favoriteService.DeleteAsync(id, UserId);
    }
}
