using System.Threading.Tasks;

namespace TestBookCatalog.Repositories.Interfaces
{
    public interface IRepositoryBase<E>
    {
        Task SaveAsync(E entity);
    }
}
