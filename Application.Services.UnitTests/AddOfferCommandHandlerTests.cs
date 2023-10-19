using Domain.Entities;
using Domain.Repositories;
using FluentValidation;
using FluentValidation.Results;
using Moq;
using ValidationException = FluentValidation.ValidationException;

namespace Application.Services.UseCases.AddOffer.Tests
{
    public class AddOfferCommandHandlerTests
    {
        [Fact]
        public async Task Handle_ValidCommand_ReturnsGuid()
        {
            // Arrange
            var productRepositoryMock = new Mock<IProductRepository>();
            var validatorMock = new Mock<IValidator<AddOfferCommand>>();

            validatorMock.Setup(v => v.ValidateAsync(It.IsAny<AddOfferCommand>(), default))
                .ReturnsAsync(new ValidationResult());

            var handler = new AddOfferCommandHandler(productRepositoryMock.Object, validatorMock.Object);

            var command = new AddOfferCommand
            {
                ProductName = "Product Name",
                ProductBrand = "Product Brand",
                ProductSize = "Product Size",
                Price = 10.99f,
                Quantity = 5.0f
            };

            var expectedProductId = Guid.NewGuid();

            productRepositoryMock.Setup(r => r.AddAsync(It.IsAny<Product>(), CancellationToken.None))
                .ReturnsAsync(expectedProductId);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.Equal(expectedProductId, result);
        }

        [Fact]
        public async Task Handle_InvalidCommand_ThrowsValidationException()
        {
            // Arrange
            var productRepositoryMock = new Mock<IProductRepository>();
            var validatorMock = new Mock<IValidator<AddOfferCommand>>();

            var validationResult = new ValidationResult(new[]
            {
                new ValidationFailure("ProductName", "Product Name is required")
            });

            validatorMock.Setup(v => v.ValidateAsync(It.IsAny<AddOfferCommand>(), default))
                .ReturnsAsync(validationResult);

            var handler = new AddOfferCommandHandler(productRepositoryMock.Object, validatorMock.Object);

            var command = new AddOfferCommand
            {
                ProductName = null, // Invalid, required field is missing
                ProductBrand = "Product Brand",
                ProductSize = "Product Size",
                Price = 10.99f,
                Quantity = 5.0f
            };

            // Act & Assert
            await Assert.ThrowsAsync<ValidationException>(() => handler.Handle(command, CancellationToken.None));
        }
    }
}
