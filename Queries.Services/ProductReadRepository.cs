using Dapper;
using Infrastructure.Persistence;
using Queries.Data;
using Queries.Interface;

namespace Queries.Services
{
    public class ProductReadRepository : IProductReadRepository
    {
        private IMyShopContext _context;
        public ProductReadRepository(IMyShopContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<OffreRow>> GetAllAsync(CancellationToken cancellationToken)
        {
            var query = "SELECT\r\n    p.\"Id\",\r\n    p.\"ProductName\",\r\n    p.\"ProductBrand\",\r\n    p.\"ProductSize\",\r\n    pr.\"PriceValue\",\r\n    s.\"Quantity\"\r\n" +
                "FROM\r\n    \"dbo.product\" p\r\n    " +
                "JOIN \"dbo.price\" pr ON p.\"PriceId\" = pr.\"Id\"\r\n    " +
                "JOIN \"dbo.stock\" s ON p.\"StockId\" = s.\"Id\";";
            using (var connection = _context.CreateConnection())
            {
                var products = await connection.QueryAsync<OffreRow>(query, cancellationToken);

                return products;
            }
        }
    }
}