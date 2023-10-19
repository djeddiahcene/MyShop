using Application.Services.UseCases.AddOffer;
using Domain.Entities;
using Domain.Repositories;
using FluentValidation;
using MediatR;

namespace Application.Services.UseCases.UpdateOffer
{
    public class UpdateOfferCommandHandler : IRequestHandler<UpdateOfferCommand, bool>
    {
        private readonly IProductRepository _productRepository;
        private readonly IValidator<UpdateOfferCommand> _validator;

        public UpdateOfferCommandHandler(IProductRepository productRepository, IValidator<UpdateOfferCommand>  validator)
        {
            _productRepository = productRepository;
            _validator = validator;
        }
        public async Task<bool> Handle(UpdateOfferCommand command, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(command);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }


            var product = await _productRepository.GetByIdAsync(command.Id, cancellationToken);

            if(product == null)
            {
                throw new ValidationException("le produit avec l'id " + command.Id + " n'existe pas");
            }

            product.ProductBrand = command.ProductBrand;
            product.ProductSize = command.ProductSize;
            product.ProductName = command.ProductName;
            product.Price.PriceValue = command.Price;
            product.Stock.Quantity = command.Quantity;

            return await _productRepository.UpdateAsync(product, cancellationToken);
        }
    }
}
