using System;
using System.Collections.Generic;
using System.Linq;
using SellerReturnApi.Core.Common;
using SellerReturnApi.Core.Returns.Events;

namespace SellerReturnApi.Core.Returns
{
    public class SellerReturn : AggregateBase
    {
        private SellerReturn()
            : base()
        {
        }

        public SellerReturn(long id, string name, long warehouseId, long dropOffPointId, DateTimeOffset moment)
            : this()
        {
            var created = new V1.ReturnCreated
            (
                id,
                name,
                warehouseId,
                dropOffPointId,
                moment
            );
            
            AddEvent(created);
            Apply(created);
        }

        public string Name { get; private set; }
        
        public long WarehouseId { get; private set; }
        
        public long DropOffPointId { get; private set; }

        public long? CellId { get; private set; }

        public CellType CellType { get; private set; }

        public DateTimeOffset? StoreMoment { get; private set; }

        public int TotalDays { get; private set; }

        public SellerReturnStatus Status { get; private set; }

        public void Move(ReturnMovement movement)
        {
            var returnMoved = new V1.ReturnMoved
            (
                Id,
                movement.WarehouseId,
                movement.CellId,
                GetCellType(movement.CellTags),
                movement.Moment
            );

            Apply(returnMoved);
            AddEvent(returnMoved);
        }
        

        public void Lose(DateTimeOffset moment)
        {
            var returnLost = new V1.ReturnLost
            (
                Id,
                moment
            );
            
            Apply(returnLost);
            AddEvent(returnLost);
        }

        public void Find(FindReturn findReturn)
        {
            var returnFound = new V1.ReturnFound
            (
                Id,
                findReturn.WarehouseId,
                findReturn.CellId,
                GetCellType(findReturn.CellTags),
                findReturn.Moment
            );
            
            Apply(returnFound);
        }

        public void ReturnToSeller(DateTimeOffset moment)
        {
            var returnedToSeller = new V1.ReturnedToSeller
            (
                Id,
                moment
            );
            
            Apply(returnedToSeller);
            AddEvent(returnedToSeller);
        }
        
        private void Apply(V1.ReturnCreated returnCreated)
        {
            Id = returnCreated.ArticleId;
            Name = returnCreated.ArticleName;
            WarehouseId = returnCreated.WarehouseId;
            DropOffPointId = returnCreated.DropOffPointId;

            Version++;
        }

        private void Apply(V1.ReturnMoved returnMoved)
        {
            if (CellType == CellType.Return && Status != SellerReturnStatus.Lost)
            {
                var diff = returnMoved.Moment - StoreMoment;
                TotalDays += diff?.Days ?? 0; 
                StoreMoment = returnMoved.Moment;
            }

            CellId = returnMoved.CellId;
            CellType = returnMoved.CellType;
            StoreMoment = returnMoved.Moment;
            Status = CalculateStatus();

            Version++;
        }

        private void Apply(V1.ReturnLost returnLost)
        {
            Status = SellerReturnStatus.Lost;

            Version++;
        }

        private void Apply(V1.ReturnFound returnFound)
        {
            WarehouseId = returnFound.WarehouseId;
            CellId = returnFound.CellId;
            CellType = returnFound.CellType;
            Status = CalculateStatus();
            
            Version++;
        }

        private void Apply(V1.ReturnedToSeller returnedToSeller)
        {
            Status = SellerReturnStatus.ReturnedToSeller;

            Version++;
        }

        private CellType GetCellType(IReadOnlyCollection<string> cellTags)
        {
            var cellType = CellType.Unknown;

            if (cellTags.Contains("Return"))
                cellType |= CellType.Return;

            if (cellTags.Contains("Utilization"))
                cellType |= CellType.Utilization;

            return cellType;
        }

        private SellerReturnStatus CalculateStatus()
        {
            return TotalDays switch
            {
                >= 0 and <= 3 => SellerReturnStatus.FreeStore,
                > 3 and <= 10 => SellerReturnStatus.ShortTermStore,
                > 10 and < 40 => SellerReturnStatus.LongTermStore,
                > 40 => SellerReturnStatus.NeededToUtilize,
                _ => SellerReturnStatus.Unknown
            };
        }
    }
}