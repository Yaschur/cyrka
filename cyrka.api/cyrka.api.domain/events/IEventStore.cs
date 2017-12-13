using System;
using System.Threading.Tasks;
using cyrka.api.domain.events;

namespace cyrka.api.domain.events
{
	public interface IEventStore
	{
		Task<ulong> GetLastStoredId();

		Task Store(Event @event);

		Task<Event[]> FindAllAfterId(ulong Id);

		IObservable<Event> AsObservable();
	}
}
