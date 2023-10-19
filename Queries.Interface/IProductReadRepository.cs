using Queries.Data;

namespace Queries.Interface
{
    public interface IProductReadRepository
    {
        Task<IEnumerable<OffreRow>> GetAllAsync(CancellationToken cancellationToken);
    }
}