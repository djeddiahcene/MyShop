using Application.Services.UseCases.UpdateOffer;
using Domain.Entities;
using Domain.Repositories;
using FluentValidation;
using FluentValidation.Results;
using Moq;

namespace Application.Services.UnitTests
{
    public class UpdateOfferCommandHandlerTests
    {
        private readonly Mock<IProductRepository> _productRepositoryMock;
        private readonly Mock<IValidator<UpdateOfferCommand>> _validatorMock;
        private readonly UpdateOfferCommandHandler _handler;

        public UpdateOfferCommandHandlerTests()
        {
            _productRepositoryMock = new Mock<IProductRepository>();
            _validatorMock = new Mock<IValidator<UpdateOfferCommand>>();
            _handler = new UpdateOfferCommandHandler(_productRepositoryMock.Object, _validatorMock.Object);
        }

        [Fact]
        public async Task Handle_WithValidCommand_ReturnsTrue()
        {
            // Arrange
            var command = new UpdateOfferCommand
            {
                Id = Guid.NewGuid(),
                // Set other command properties as needed
            };

            var validationResult = new ValidationResult();

            _validatorMock.Setup(v => v.ValidateAsync(command, default))
                .ReturnsAsync(validationResult);

            var existingProduct = new Product
            {
                Id = command.Id,
                Price = new Price(),
                Stock = new Stock()
            };

            _productRepositoryMock.Setup(r => r.GetByIdAsync(command.Id, CancellationToken.None))
                .ReturnsAsync(existingProduct);

            _productRepositoryMock.Setup(r => r.UpdateAsync(existingProduct, CancellationToken.None))
                .ReturnsAsync(true);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task Handle_WithInvalidCommand_ThrowsValidationException()
        {
            // Arrange
            var command = new UpdateOfferCommand
            {
                Id = Guid.NewGuid(),
                // Set other command properties as needed
            };

            var validationResult = new ValidationResult();
            validationResult.Errors.Add(new ValidationFailure("PropertyName", "Error message"));

            _validatorMock.Setup(v => v.ValidateAsync(command, default))
                .ReturnsAsync(validationResult);

            // Act & Assert
            await Assert.ThrowsAsync<ValidationException>(() => _handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_WithNonExistingProduct_ThrowsValidationException()
        {
            // Arrange
            var command = new UpdateOfferCommand
            {
                Id = Guid.NewGuid(),
                // Set other command properties as needed
            };

            var validationResult = new ValidationResult();

            _validatorMock.Setup(v => v.ValidateAsync(command, default))
                .ReturnsAsync(validationResult);

            _productRepositoryMock.Setup(r => r.GetByIdAsync(command.Id, CancellationToken.None))
                .ReturnsAsync((Product)null);

            // Act & Assert
            await Assert.ThrowsAsync<ValidationException>(() => _handler.Handle(command, CancellationToken.None));
        }
    }
}
