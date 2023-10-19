
using Dapper;
using Domain.Entities;
using Moq;
using Moq.Dapper;
using System.Data;

namespace Infrastructure.Persistence.Repositories.Tests
{
    public class ProductRepositoryTests
    {
        private readonly Mock<IMyShopContext> _contextMock;

        public ProductRepositoryTests()
        {
            _contextMock = new Mock<IMyShopContext>();
        }

        [Fact]
        public async Task AddAsync_ReturnsGuid()
        {
            // Arrange
            var _contextMock = new Mock<IMyShopContext>();
            var connectionMock = new Mock<IDbConnection>();
            var product = new Product
            {
                Id = Guid.NewGuid(),
                ProductName = "Product Name",
                ProductBrand = "Product Brand",
                ProductSize = "Product Size",
                Price = new Price { Id = Guid.NewGuid(), PriceValue = 10.99f },
                Stock = new Stock { Id = Guid.NewGuid(), Quantity = 5.0f }
            };
            var expectedGuid = Guid.NewGuid();

            connectionMock.SetupDapperAsync(c => c.ExecuteAsync(It.IsAny<string>(), It.IsAny<object>(), null, null, null))
                .ReturnsAsync(1);

            _contextMock.Setup(c => c.CreateConnection()).Returns(connectionMock.Object);

            var repository = new ProductRepository(_contextMock.Object);

            // Act
            var result = await repository.AddAsync(product,CancellationToken.None);

            // Assert
            Assert.Equal(product.Id, result);
        }
    }
}
