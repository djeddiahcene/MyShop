using MediatR;

namespace Application.Services.UseCases.AddOffer
{
    public class AddOfferCommand : IRequest<Guid>
    {
        public string? ProductName { get; set; }
        public string? ProductBrand { get; set; }
        public string? ProductSize { get; set; }
        public float Price { get; set; }
        public float Quantity { get; set; }
    }
}
