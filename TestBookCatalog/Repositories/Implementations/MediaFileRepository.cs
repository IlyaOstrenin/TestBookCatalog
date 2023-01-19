using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using TestBookCatalog.Models;
using TestBookCatalog.Repositories.Interfaces;

namespace TestBookCatalog.Repositories.Implementations
{
    public class MediaFileRepository : IMediaFileRepository
    {
        private readonly TestBookCatalogDbContext _context;

        public MediaFileRepository(TestBookCatalogDbContext context)
        {
            _context = context;
        }

        public async Task<MediaFile> GetByIdAsync(Guid fileId)
        {
            return await _context.MediaFiles.FirstOrDefaultAsync(x => x.Id == fileId);
        }

        public async Task SaveAsync(MediaFile file)
        {
            await _context.MediaFiles.AddAsync(file);
            await _context.SaveChangesAsync();
        }
    }
}
