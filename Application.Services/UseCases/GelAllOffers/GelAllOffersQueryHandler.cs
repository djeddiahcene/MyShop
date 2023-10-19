using MediatR;
using Queries.Data;
using Queries.Interface;

namespace Application.Services.UseCases.GelAllOffers
{
    public class GelAllOffersQueryHandler : IRequestHandler<GelAllOffersQuery, IEnumerable<OffreRow>>
    {
        private readonly IProductReadRepository _productReadRepository;

        public GelAllOffersQueryHandler(IProductReadRepository productReadRepository)
        {
            _productReadRepository = productReadRepository;
        }

        public async Task<IEnumerable<OffreRow>> Handle(GelAllOffersQuery request, CancellationToken cancellationToken)
        {
            return await _productReadRepository.GetAllAsync(cancellationToken);
        }
    }
}
