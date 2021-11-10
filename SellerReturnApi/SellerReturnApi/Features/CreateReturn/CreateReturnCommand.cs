using System;
using MediatR;
using SellerReturnApi.Core.Returns;

namespace SellerReturnApi.Features.CreateReturn
{
    public class CreateReturnCommand : IRequest<SellerReturn>
    {
        public long ArticleId { get; set; }
        
        public string ArticleName { get; set; }
        
        public long WarehouseId { get; set; }
        
        public long DropOffPointId { get; set; }

        public DateTimeOffset Moment { get; set; }
    }
}