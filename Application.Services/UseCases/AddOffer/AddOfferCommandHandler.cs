using Domain.Entities;
using Domain.Repositories;
using FluentValidation;
using MediatR;
using ValidationException = FluentValidation.ValidationException;

namespace Application.Services.UseCases.AddOffer
{
    public class AddOfferCommandHandler : IRequestHandler<AddOfferCommand, Guid>
    {
        private readonly IProductRepository _productRepository;
        private readonly IValidator<AddOfferCommand> _validator;

        public AddOfferCommandHandler(IProductRepository productRepository, IValidator<AddOfferCommand> validator)
        {
            _productRepository = productRepository;
            _validator = validator;
        }

        public async Task<Guid> Handle(AddOfferCommand command, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(command);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var price = new Price();
            price.Id = Guid.NewGuid();
            price.PriceValue = command.Price;


            var stock = new Stock();
            stock.Id = Guid.NewGuid();
            stock.Quantity = command.Quantity;

            Product product = new Product();
            product.Id = Guid.NewGuid();
            product.ProductName = command.ProductName;
            product.ProductBrand = command.ProductBrand;
            product.Stock = stock;
            product.Price = price;

            return await _productRepository.AddAsync(product, cancellationToken);
        }
    }
}
