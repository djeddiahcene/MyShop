using Dapper;
using Infrastructure.Persistence;
using Moq;
using Moq.Dapper;
using Queries.Data;
using System.Data;

namespace Queries.Services.UnitTests
{
    public class ProductReadRepositoryTests
    {
        [Fact]
        public async Task GetAllAsync_ReturnsListOfOffers()
        {
            // Arrange
            var contextMock = new Mock<IMyShopContext>();
            var connectionMock = new Mock<IDbConnection>();
            var expectedOffers = new List<OffreRow>
            {
                new OffreRow { Id = Guid.NewGuid(), ProductName = "Offer 1" },
                new OffreRow { Id = Guid.NewGuid(), ProductName = "Offer 2" }
            };


            connectionMock.SetupDapperAsync(c => c.QueryAsync<OffreRow>(It.IsAny<string>(), null, null, null, null))
              .ReturnsAsync(expectedOffers);

            contextMock.Setup(c => c.CreateConnection())
                .Returns(connectionMock.Object);

            var repository = new ProductReadRepository(contextMock.Object);

            // Act
            var result = await repository.GetAllAsync(CancellationToken.None);

            // Assert
            Assert.Equal(expectedOffers.First().Id, result.First().Id);
        }
    }
}
