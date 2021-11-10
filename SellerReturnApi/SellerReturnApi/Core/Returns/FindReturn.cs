using System;
using System.Collections.Generic;

namespace SellerReturnApi.Core.Returns
{
    public sealed class FindReturn
    {
        public long WarehouseId { get; set; }

        public long CellId { get; set; }

        public List<string> CellTags { get; set; } = new();

        public DateTimeOffset Moment { get; set; }
    }
}