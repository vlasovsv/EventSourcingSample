using System;
using MediatR;

namespace SellerReturnApi.Features.ReturnToSeller
{
    public class ReturnToSellerCommand : IRequest
    {
        public long ArticleId { get; set; }

        public DateTimeOffset Moment { get; set; }
    }
}