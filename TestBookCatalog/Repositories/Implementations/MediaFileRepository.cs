using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using TestBookCatalog.Models;
using TestBookCatalog.Repositories.Interfaces;

namespace TestBookCatalog.Repositories.Implementations
{
    public class MediaFileRepository : IMediaFileRepository
    {
        public async Task<MediaFile> GetByIdAsync(Guid fileId)
        {
            using (var _context = new TestBookCatalogDbContext())
            {
                return await _context.MediaFiles.FirstOrDefaultAsync(x => x.Id == fileId);
            }
        }

        public async Task SaveAsync(MediaFile file)
        {
            using (var _context = new TestBookCatalogDbContext())
            {
                await _context.MediaFiles.AddAsync(file);
                await _context.SaveChangesAsync();
            }
        }
    }
}
