using System.Threading.Tasks;
using cyrka.api.domain.events;

namespace cyrka.api.domain
{
	public interface IEventStore
	{
		Task Store(Event @event);

		Task<Event[]> FindAllByAggregateIdOf(string aggregateType, string aggregateId);
	}
}
