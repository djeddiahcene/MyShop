using Application.Services.UseCases.GelAllOffers;
using Moq;
using Queries.Data;
using Queries.Interface;

namespace Application.Services.UnitTests
{
    public class GelAllOffersQueryHandlerTests
    {
        [Fact]
        public async Task Handle_ReturnsListOfOffers()
        {
            // Arrange
            var productReadRepositoryMock = new Mock<IProductReadRepository>();
            var expectedOffers = new List<OffreRow>
        {
            new OffreRow { Id = Guid.NewGuid(), ProductName = "Offer 1" },
            new OffreRow { Id = Guid.NewGuid(), ProductName = "Offer 2" }
        };

            productReadRepositoryMock.Setup(r => r.GetAllAsync(CancellationToken.None))
                .ReturnsAsync(expectedOffers);

            var queryHandler = new GelAllOffersQueryHandler(productReadRepositoryMock.Object);

            // Act
            var result = await queryHandler.Handle(new GelAllOffersQuery(), CancellationToken.None);

            // Assert
            Assert.Equal(expectedOffers, result);
        }
    }
}