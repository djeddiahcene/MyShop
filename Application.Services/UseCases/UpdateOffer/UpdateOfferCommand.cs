using MediatR;

namespace Application.Services.UseCases.UpdateOffer
{
    public class UpdateOfferCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
        public string? ProductName { get; set; }
        public string? ProductBrand { get; set; }
        public string? ProductSize { get; set; }
        public float Price { get; set; }
        public float Quantity { get; set; }
    }
}
