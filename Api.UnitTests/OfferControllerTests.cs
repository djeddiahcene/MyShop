using Api.Controllers;
using Application.Services.UseCases.AddOffer;
using Application.Services.UseCases.GelAllOffers;
using Application.Services.UseCases.UpdateOffer;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Queries.Data;

namespace Api.UnitTests
{
    public class OfferControllerTests
    {
        [Fact]
        public async Task GetAll_ReturnsOkResultWithOffers()
        {
            // Arrange
            var mediatorMock = new Mock<IMediator>();
            var expectedOffers = new List<OffreRow>
            {
                new OffreRow { Id = Guid.NewGuid(), ProductName = "Offer 1" },
                new OffreRow { Id = Guid.NewGuid(), ProductName = "Offer 2" }
            };

            mediatorMock.Setup(m => m.Send(It.IsAny<GelAllOffersQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedOffers);

            var controller = new offerController(mediatorMock.Object);

            // Act
            var result = await controller.GetAll();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var offers = Assert.IsAssignableFrom<IEnumerable<OffreRow>>(okResult.Value);

            Assert.Equal(expectedOffers, offers);
        }

        [Fact]
        public async Task AddOffer_ReturnsOkResultWithGuid()
        {
            // Arrange
            var mediatorMock = new Mock<IMediator>();
            var offerId = Guid.NewGuid();

            mediatorMock.Setup(m => m.Send(It.IsAny<AddOfferCommand>(), default))
                .ReturnsAsync(offerId);

            var controller = new offerController(mediatorMock.Object);

            var addOfferCommand = new AddOfferCommand
            {
                ProductName = "Product Name",
                ProductBrand = "Product Brand",
                ProductSize = "Product Size",
                Price = 10.99f,
                Quantity = 5.0f
            };

            // Act
            var result = await controller.AddOffer(addOfferCommand);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(offerId, okResult.Value);
        }

        [Fact]
        public async Task UpdateOffer_ReturnsOkResult()
        {
            // Arrange
            var mediatorMock = new Mock<IMediator>();
            var offerId = Guid.NewGuid();

            // Arrange
            var updateOfferCommand = new UpdateOfferCommand
            {
                ProductName = "Product Name",
                ProductBrand = "Product Brand",
                ProductSize = "Product Size",
                Price = 10.99f,
                Quantity = 5.0f
            };

            var expectedResult = true;

            mediatorMock.Setup(m => m.Send(updateOfferCommand, default)).ReturnsAsync(expectedResult);

            var controller = new offerController(mediatorMock.Object);

            // Act
            var result = await controller.UpdateOffer(updateOfferCommand);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(expectedResult, okResult.Value);
        }
    }
}
