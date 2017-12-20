using System;
using System.Threading.Tasks;

namespace cyrka.api.common.events
{
	public interface IEventStore
	{
		Task<ulong> GetLastStoredId();

		Task Store(Event @event);

		Task<Event[]> FindAllAfterId(ulong Id);

		IObservable<Event> AsObservable();
	}
}
