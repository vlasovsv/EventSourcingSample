using System.Threading;
using System.Threading.Tasks;
using Marten;
using MediatR;
using SellerReturnApi.Core.Returns;

namespace SellerReturnApi.Features.GetReturn
{
    public class GetReturnQueryHandler : IRequestHandler<GetReturnQuery, SellerReturn>
    {
        private readonly IDocumentStore _store;

        public GetReturnQueryHandler(IDocumentStore store)
        {
            _store = store;
        }
        
        public async Task<SellerReturn> Handle(GetReturnQuery request, CancellationToken cancellationToken)
        {
            await using var session = _store.LightweightSession();

            var sellerReturn = await session.Events.AggregateStreamAsync<SellerReturn>(
                request.Id.ToString(), token: cancellationToken);

            return sellerReturn;
        }
    }
}