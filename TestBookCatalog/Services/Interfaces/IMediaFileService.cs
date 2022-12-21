using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace TestBookCatalog.Services.Interfaces
{
    public interface IMediaFileService
    {
        Task<Guid> UploadMediaFileAsync(IFormFile media);
    }
}
