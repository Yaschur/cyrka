using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace cyrka.api.common.events
{
	public interface IEventStore
	{
		string NexterChannelKey { get; }

		Task<ulong> GetLastStoredId();

		Task Store(Event @event);

		Task<Event[]> FindAllAfterId(ulong Id);

		Task<Event[]> FindAllOfAggregateById<TAggregate>(string aggregateId)
			where TAggregate : class;

		Task<Event[]> FindLastNWithDataOf<TEventData>(int n, Expression<Func<Event, bool>> eventPredicate = null)
			where TEventData : EventData;

		IObservable<Event> AsObservable(bool fromStart = false);
	}
}
