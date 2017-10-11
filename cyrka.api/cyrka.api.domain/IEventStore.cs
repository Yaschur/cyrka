using System.Threading.Tasks;
using cyrka.api.domain.events;

namespace cyrka.api.domain
{
	public interface IEventStore
	{
		Task Store(Event @event);

		Task<TAggregate[]> FindAllByAggregateIdOf<TAggregate>(string aggregateId);
	}
}
