using System;
using System.Collections.Generic;
using MediatR;

namespace SellerReturnApi.Features.MoveReturn
{
    public class MoveReturnCommand : IRequest
    {
        public long ArticleId { get; set; }

        public long WarehouseId { get; set; }

        public long CellId { get; set; }

        public List<string> CellTags { get; set; } = new();

        public DateTimeOffset Moment { get; set; }
    }
}