using System;
using System.Threading;
using System.Threading.Tasks;
using Marten;
using MediatR;
using SellerReturnApi.Core.Returns;

namespace SellerReturnApi.Features.CreateReturn
{
    public class CreateReturnCommandHandler : IRequestHandler<CreateReturnCommand, SellerReturn>
    {
        private readonly IDocumentStore _store;

        public CreateReturnCommandHandler(IDocumentStore store)
        {
            _store = store;
        }

        public async Task<SellerReturn> Handle(CreateReturnCommand request, CancellationToken cancellationToken)
        {
            await using var session = _store.LightweightSession();

            var sellerReturn = new SellerReturn
            (
                request.ArticleId,
                request.ArticleName,
                request.WarehouseId,
                request.DropOffPointId,
                request.Moment
            );

            session.Events.StartStream<SellerReturn>(sellerReturn.Id.ToString(), sellerReturn.UncommittedEvents);

            await session.SaveChangesAsync(cancellationToken);
            sellerReturn.ClearUncommittedEvents();
            return sellerReturn;
        }
    }
}