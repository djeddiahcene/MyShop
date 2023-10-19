using FluentValidation;

namespace Application.Services.UseCases.UpdateOffer
{
    public class UpdateOfferCommandValidator : AbstractValidator<UpdateOfferCommand>
    {
        public UpdateOfferCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
        }
    }
}
