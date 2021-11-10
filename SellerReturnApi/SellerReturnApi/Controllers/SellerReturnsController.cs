using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SellerReturnApi.Core.Returns;
using SellerReturnApi.Features.CreateReturn;
using SellerReturnApi.Features.GetReturn;
using SellerReturnApi.Features.LoseReturn;
using SellerReturnApi.Features.MoveReturn;
using SellerReturnApi.Features.ReturnToSeller;

namespace SellerReturnApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SellerReturnsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SellerReturnsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("create-return")]
        public async Task<ActionResult<SellerReturn>> CreateReturn(CreateReturnCommand returnCommand, CancellationToken cancellationToken)
        {
            var sellerReturn = await _mediator.Send(returnCommand, cancellationToken);

            return sellerReturn;
        }
        
        [HttpGet("{id:long}")]
        public async Task<ActionResult<SellerReturn>> GetReturn(long id, CancellationToken cancellationToken)
        {
            var sellerReturn = await _mediator.Send(new GetReturnQuery() { Id = id}, cancellationToken);

            return sellerReturn;
        }
        
        [HttpPost("move")]
        public async Task<ActionResult> MoveReturn(MoveReturnCommand moveReturnCommand, CancellationToken cancellationToken)
        {
           await _mediator.Send(moveReturnCommand, cancellationToken);

           return Ok();
        }
        
        [HttpPost("return-to-seller")]
        public async Task<ActionResult> ReturnToSeller(ReturnToSellerCommand returnToSellerCommand, CancellationToken cancellationToken)
        {
            await _mediator.Send(returnToSellerCommand, cancellationToken);

            return Ok();
        }
        
        [HttpPost("lose-return")]
        public async Task<ActionResult> LoseReturn(LoseReturnCommand loseReturnCommand, CancellationToken cancellationToken)
        {
            await _mediator.Send(loseReturnCommand, cancellationToken);

            return Ok();
        }
    }
}