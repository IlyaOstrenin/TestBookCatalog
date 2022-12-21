using System;
using System.Threading.Tasks;
using TestBookCatalog.Models;

namespace TestBookCatalog.Repositories.Interfaces
{
    public interface IMediaFileRepository : IRepositoryBase<MediaFile>
    {
        Task<MediaFile> GetByIdAsync(Guid fileId);
    }
}
