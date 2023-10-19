using Domain.Entities;

namespace Domain.Repositories
{
    public interface IProductRepository
    {
        Task<Guid> AddAsync(Product entity, CancellationToken cancellationToken);
        Task<Product> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<bool> UpdateAsync(Product entity, CancellationToken cancellationToken);
    }
}
