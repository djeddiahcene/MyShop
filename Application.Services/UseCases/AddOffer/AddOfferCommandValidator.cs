using FluentValidation;

namespace Application.Services.UseCases.AddOffer
{
    public class AddOfferCommandValidator : AbstractValidator<AddOfferCommand>
    {
        public AddOfferCommandValidator()
        {
            RuleFor(x => x.ProductName).NotEmpty();
        }
    }
}
