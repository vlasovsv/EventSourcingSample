using System.Threading;
using System.Threading.Tasks;
using Marten;
using MediatR;
using SellerReturnApi.Core.Returns;

namespace SellerReturnApi.Features.ReturnToSeller
{
    public class ReturnToSellerCommandHandler : AsyncRequestHandler<ReturnToSellerCommand>
    {
        private readonly IDocumentStore _store;

        public ReturnToSellerCommandHandler(IDocumentStore store)
        {
            _store = store;
        }

        protected override async Task Handle(ReturnToSellerCommand request, CancellationToken cancellationToken)
        {
            await using var session = _store.LightweightSession();
            var sellerReturn = await 
                session.Events.AggregateStreamAsync<SellerReturn>(request.ArticleId.ToString(), token: cancellationToken);
            
            sellerReturn.ReturnToSeller(request.Moment);

            var events = sellerReturn.UncommittedEvents;
            session.Events.Append(sellerReturn.Id.ToString(), sellerReturn.Version, events);

            await session.SaveChangesAsync(cancellationToken);
            sellerReturn.ClearUncommittedEvents();
        }
    }
}