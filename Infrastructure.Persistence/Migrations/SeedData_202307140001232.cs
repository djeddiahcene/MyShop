using FluentMigrator;

namespace Infrastructure.Persistence.Migrations
{
    [Migration(202307140001232, "SeedData_202307140001232")]
    public class SeedData_202307140001232 : Migration
    {
        private Random random = new Random();

        public override void Up()
        {
            for (int i = 1; i <= 100; i++)
            {
                Guid priceId = Guid.NewGuid();
                Guid stockId = Guid.NewGuid();

                Insert.IntoTable("dbo.price")
                    .Row(new
                    {
                        Id = priceId,
                        PriceValue = i * 10,
                        CreatedDate = DateTime.UtcNow,
                        CreatedBy = "System",
                        ModifiedDate = DateTime.UtcNow,
                        ModifiedBy = "System"
                    });

                Insert.IntoTable("dbo.stock")
                    .Row(new
                    {
                        Id = stockId,
                        Quantity = i * 100,
                        CreatedDate = DateTime.UtcNow,
                        CreatedBy = "System",
                        ModifiedDate = DateTime.UtcNow,
                        ModifiedBy = "System"
                    });

                Insert.IntoTable("dbo.product")
                    .Row(new
                    {
                        Id = Guid.NewGuid(),
                        ProductName = $"Product {i}",
                        ProductBrand = $"Brand {i}",
                        ProductSize = $"Size {i}",
                        PriceId = priceId,
                        StockId = stockId,
                        CreatedDate = DateTime.UtcNow,
                        CreatedBy = "System",
                        ModifiedDate = DateTime.UtcNow,
                        ModifiedBy = "System"
                    });
            }
        }

        public override void Down()
        {
            Delete.FromTable("dbo.product");
            Delete.FromTable("dbo.stock");
            Delete.FromTable("dbo.price");
        }
    }
}
