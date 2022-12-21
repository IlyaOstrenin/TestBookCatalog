using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;
using TestBookCatalog.Models;
using TestBookCatalog.Repositories.Interfaces;
using TestBookCatalog.Services.Interfaces;

namespace TestBookCatalog.Services.Implementations
{
    public class MediaFileService : IMediaFileService
    {
        private readonly IMediaFileRepository _mediaFileRepository;

        public MediaFileService(IMediaFileRepository mediaFileRepository)
        {
            _mediaFileRepository = mediaFileRepository;
        }

        public async Task<Guid> UploadMediaFileAsync(IFormFile file)
        {
            if (file.Length == 0)
                throw new InvalidOperationException("The file is empty");

            if (file.ContentType != "image/jpeg")
                throw new InvalidOperationException("The file format is not supported");

            string fileExtension = Path.GetExtension(file.FileName).ToLower();

            var mediaFile = new MediaFile();
            mediaFile.Path = Path.Combine(Directory.GetCurrentDirectory(), "MediaFiles", $"{mediaFile.Id}{fileExtension}");

            if (!Directory.Exists(Path.GetDirectoryName(mediaFile.Path)))
                Directory.CreateDirectory(Path.GetDirectoryName(mediaFile.Path));

            using (var fileStream = new FileStream(mediaFile.Path, FileMode.Create))
                await file.CopyToAsync(fileStream);

            await _mediaFileRepository.SaveAsync(mediaFile);

            return mediaFile.Id;
        }
    }
}
