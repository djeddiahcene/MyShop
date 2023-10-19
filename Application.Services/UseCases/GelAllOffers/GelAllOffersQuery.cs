using MediatR;
using Queries.Data;

namespace Application.Services.UseCases.GelAllOffers
{
    public class GelAllOffersQuery : IRequest<IEnumerable<OffreRow>>
    {
    }
}
