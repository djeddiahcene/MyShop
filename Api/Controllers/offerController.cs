using Application.Services.UseCases.AddOffer;
using Application.Services.UseCases.GelAllOffers;
using Application.Services.UseCases.UpdateOffer;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Queries.Data;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class offerController : ControllerBase
    {
        private readonly IMediator _mediator;
        public offerController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OffreRow>>> GetAll()
        {
            var result = await _mediator.Send(new GelAllOffersQuery());
            return Ok(result);
        }

        [HttpPost("AddOffer")]
        public async Task<ActionResult<Guid>> AddOffer([FromBody] AddOfferCommand addOfferCommand)
        {

            var result = await _mediator.Send(addOfferCommand);
            return Ok(result);
        }

        [HttpPost("UpdateOffer")]
        public async Task<ActionResult<bool>> UpdateOffer([FromBody] UpdateOfferCommand updateOfferCommand)
        {

            var result = await _mediator.Send(updateOfferCommand);
            return Ok(result);
        }

    }
}
