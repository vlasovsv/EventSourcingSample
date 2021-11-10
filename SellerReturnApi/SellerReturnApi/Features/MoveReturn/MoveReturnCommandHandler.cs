using System.Threading;
using System.Threading.Tasks;
using Marten;
using MediatR;
using SellerReturnApi.Core.Returns;

namespace SellerReturnApi.Features.MoveReturn
{
    public class MoveReturnCommandHandler : AsyncRequestHandler<MoveReturnCommand>
    {
        private readonly IDocumentStore _store;

        public MoveReturnCommandHandler(IDocumentStore store)
        {
            _store = store;
        }
        
        protected override async Task Handle(MoveReturnCommand request, CancellationToken cancellationToken)
        {
            await using var session = _store.LightweightSession();
            var sellerReturn = await 
                session.Events.AggregateStreamAsync<SellerReturn>(request.ArticleId.ToString(), token: cancellationToken);
            
            sellerReturn.Move(new ReturnMovement()
            {
                WarehouseId = request.WarehouseId,
                CellId = request.CellId,
                CellTags = request.CellTags,
                Moment = request.Moment
            });

            var events = sellerReturn.UncommittedEvents;
            session.Events.Append(sellerReturn.Id.ToString(), sellerReturn.Version, events);

            await session.SaveChangesAsync(cancellationToken);
            sellerReturn.ClearUncommittedEvents();
        }
    }
}