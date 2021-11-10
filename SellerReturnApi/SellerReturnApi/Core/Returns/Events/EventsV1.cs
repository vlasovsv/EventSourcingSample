using System;

namespace SellerReturnApi.Core.Returns.Events
{
    public static class V1
    {
        public record ReturnCreated(
            long ArticleId,
            string ArticleName,
            long WarehouseId,
            long DropOffPointId,
            DateTimeOffset Moment
        );


        public record ReturnMoved(
            long ArticleId,
            long WarehouseId,
            long CellId,
            CellType CellType,
            DateTimeOffset Moment
        );

        public record ReturnLost(
            long ArticleId,
            DateTimeOffset Moment
        );

        public record ReturnFound(
            long ArticleId,
            long WarehouseId,
            long CellId,
            CellType CellType,
            DateTimeOffset Moment
        );

        public record ReturnedToSeller(
            long ArticleId,
            DateTimeOffset Moment
        );

        public record FreeStoragePeriodStarted(
            long ArticleId,
            DateTimeOffset Moment
        );
        
        public record ShortTermStoragePeriodStarted(
            long ArticleId,
            DateTimeOffset Moment
        );
        
        public record LongTermStoragePeriodStarted(
            long ArticleId,
            DateTimeOffset Moment
        );
    }
}