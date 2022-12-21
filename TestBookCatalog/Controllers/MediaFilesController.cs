using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using TestBookCatalog.Services.Interfaces;

namespace TestBookCatalog.Controllers
{
    [ApiController]
    [Authorize(Roles = "admin")]
    [Route("api/[controller]")]
    public class MediaFilesController : ControllerBase
    {
        private readonly IMediaFileService _mediaFileService;

        public MediaFilesController(IMediaFileService mediaFileService)
        {
            _mediaFileService = mediaFileService;
        }

        /// <summary>
        /// upload media files
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<Guid> Post([Required] IFormFile file) => await _mediaFileService.UploadMediaFileAsync(file);
    }
}
