using System.Collections.Generic;

namespace SellerReturnApi.Core.Common
{
    public abstract class AggregateBase
    {
        private readonly List<object> _events;

        protected AggregateBase()
        {
            _events = new List<object>();
        }

        public long Id { get; protected set; }

        public long Version { get; protected set; }
        
        public IReadOnlyCollection<object> UncommittedEvents => _events.AsReadOnly();

        public void ClearUncommittedEvents() => _events.Clear();

        protected void AddEvent(object domainEvent) => _events.Add(domainEvent);

        protected void AddEvents(params object[] events) => _events.AddRange(events);
    }
}