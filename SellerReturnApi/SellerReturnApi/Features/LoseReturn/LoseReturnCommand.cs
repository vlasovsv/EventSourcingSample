using System;
using MediatR;

namespace SellerReturnApi.Features.LoseReturn
{
    public class LoseReturnCommand : IRequest
    {
        public long ArticleId { get; set; }

        public DateTimeOffset Moment { get; set; }
    }
}