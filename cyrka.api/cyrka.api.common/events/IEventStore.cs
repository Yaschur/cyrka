using System;
using System.Threading.Tasks;

namespace cyrka.api.common.events
{
	public interface IEventStore
	{
		Task<ulong> GetLastStoredId();

		Task Store(Event @event);

		Task<Event[]> FindAllAfterId(ulong Id);

		Task<Event[]> FindAllOfAggregateById<TAggregate>(string aggregateId)
			where TAggregate : class;

		IObservable<Event> AsObservable();
	}
}
