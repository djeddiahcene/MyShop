using Dapper;
using Domain.Entities;
using Domain.Repositories;
using Infrastructure.Persistence.Extensions;
using static Dapper.SqlMapper;

namespace Infrastructure.Persistence.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private IMyShopContext _context;


        public ProductRepository(IMyShopContext dapperContext)
        {
            _context = dapperContext;
        }

        public async Task<Guid> AddAsync(Product entity, CancellationToken cancellationToken)
        {
            var sql = "INSERT INTO \"dbo.stock\" (\"Id\", \"Quantity\") VALUES (@StockId, @Quantity);" +
                      "INSERT INTO \"dbo.price\"  (\"Id\", \"PriceValue\") VALUES (@PriceId, @PriceValue);" +
                      "INSERT INTO \"dbo.product\" (\"Id\", \"ProductName\", \"ProductBrand\", \"ProductSize\", \"PriceId\", \"StockId\")" +
                      "VALUES (@Id, @ProductName, @ProductBrand, @ProductSize, @PriceId, @StockId);";


            using (var connection = _context.CreateConnection())
            {
                connection.Open();

                try
                {
                    var result = await connection.ExecuteAsync(sql, new
                    {

                        StockId = entity.Stock.Id,
                        entity.Stock.Quantity,
                        PriceId = entity.Price.Id,
                        entity.Price.PriceValue,
                        entity.Id,
                        entity.ProductName,
                        entity.ProductBrand,
                        entity.ProductSize,
                    });
                }
                catch (Exception)
                {

                    throw;
                }



                return entity.Id;
            }
        }

        public async Task<Product> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var sql = "SELECT p.*, pr.\"Id\" , s.\"Id\" " +
                "FROM \"dbo.product\" p " +
                "JOIN \"dbo.stock\" s ON p.\"StockId\" = s.\"Id\" " +
                "JOIN \"dbo.price\" pr ON p.\"PriceId\" = pr.\"Id\" " +
                "WHERE p.\"Id\" = @Id; ";

            using (var connection = _context.CreateConnection())
            {
                connection.Open();

                var result = await connection.QueryAsync<Product, Guid, Guid, Product>(
                    sql,
                    (product, priceId, stockId) =>
                    {
                        product.Price = new Price() { Id = priceId };
                        product.Stock = new Stock() { Id = stockId };
                        return product;
                    },
                     new
                     {
                         Id = id
                     }
                );

                return result.FirstOrDefault();
            }

        }

        public async Task<bool> UpdateAsync(Product entity, CancellationToken cancellationToken)
        {
            var sql = @"UPDATE ""dbo.product"" 
                SET ""ProductName"" = @ProductName, 
                    ""ProductBrand"" = @ProductBrand, 
                    ""ProductSize"" = @ProductSize
                WHERE ""Id"" = @Id;
                
                UPDATE ""dbo.price"" 
                SET ""PriceValue"" = @PriceValue 
                WHERE ""Id"" = @PriceId;
                
                UPDATE ""dbo.stock"" 
                SET ""Quantity"" = @Quantity 
                WHERE ""Id"" = @StockId;";

            using (var connection = _context.CreateConnection())
            {
                connection.Open();
                var result = await connection.ExecuteAsync(sql, new
                {
                    entity.ProductName,
                    entity.ProductBrand,
                    entity.ProductSize,
                    entity.Id,
                    entity.Price.PriceValue,
                    entity.Stock.Quantity,
                    PriceId = entity.Price.Id,
                    StockId = entity.Stock.Id
                });

                return result == 3;
            }

        }
    }

}
