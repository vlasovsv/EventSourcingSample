using System;

namespace SellerReturnApi.Core.Returns
{
    [Flags]
    public enum CellType
    {
        Unknown,
        Return,
        Utilization
    }
}