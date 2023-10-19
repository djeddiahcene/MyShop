using FluentMigrator;

namespace Infrastructure.Persistence.Migrations
{
    [Migration(202307140001231, "InitialTables_202307140001231")]
    public class InitialTables_202307140001231 : Migration
    {
        public override void Down()
        {
            Delete.Table("dbo.product");
            Delete.Table("dbo.price");
            Delete.Table("dbo.stock");

            Delete.Index("idx_product_priceid").OnTable("dbo.product");
            Delete.Index("idx_product_stockid").OnTable("dbo.product");

        }


        public override void Up()
        {
            Create.Table("dbo.price")
                .WithColumn("Id").AsGuid().NotNullable().PrimaryKey()
                .WithColumn("PriceValue").AsFloat().NotNullable()
                .WithColumn("CreatedDate").AsDateTime().NotNullable().WithDefault(SystemMethods.CurrentUTCDateTime)
                .WithColumn("CreatedBy").AsString().NotNullable().WithDefaultValue("System")
                .WithColumn("ModifiedDate").AsDateTime().NotNullable().WithDefault(SystemMethods.CurrentUTCDateTime)
                .WithColumn("ModifiedBy").AsString().NotNullable().WithDefaultValue("System");

            Create.Table("dbo.stock")
                .WithColumn("Id").AsGuid().NotNullable().PrimaryKey()
                .WithColumn("Quantity").AsFloat().NotNullable()
                .WithColumn("CreatedDate").AsDateTime().NotNullable().WithDefault(SystemMethods.CurrentUTCDateTime)
                .WithColumn("CreatedBy").AsString().NotNullable().WithDefaultValue("System")
                .WithColumn("ModifiedDate").AsDateTime().NotNullable().WithDefault(SystemMethods.CurrentUTCDateTime)
                .WithColumn("ModifiedBy").AsString().NotNullable().WithDefaultValue("System");

            Create.Table("dbo.product")
                .WithColumn("Id").AsGuid().NotNullable().PrimaryKey()
                .WithColumn("ProductName").AsString(50).NotNullable()
                .WithColumn("ProductBrand").AsString(60).Nullable()
                .WithColumn("ProductSize").AsString(50).Nullable()
                .WithColumn("PriceId").AsGuid().NotNullable().ForeignKey("dbo.price", "Id")
                .WithColumn("StockId").AsGuid().NotNullable().ForeignKey("dbo.stock", "Id")
                .WithColumn("CreatedDate").AsDateTime().NotNullable().WithDefault(SystemMethods.CurrentUTCDateTime)
                .WithColumn("CreatedBy").AsString().NotNullable().WithDefaultValue("System")
                .WithColumn("ModifiedDate").AsDateTime().NotNullable().WithDefault(SystemMethods.CurrentUTCDateTime)
                .WithColumn("ModifiedBy").AsString().NotNullable().WithDefaultValue("System");

            Create.Index("idx_product_priceid").OnTable("dbo.product").OnColumn("PriceId");
            Create.Index("idx_product_stockid").OnTable("dbo.product").OnColumn("StockId");


        }
    }
}
