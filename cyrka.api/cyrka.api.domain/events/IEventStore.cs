using System.Threading.Tasks;

namespace cyrka.api.domain.events
{
	public interface IEventStore
	{
		Task Store(Event @event);

		Task<TAggEvent[]> FindAllByAggregateOf<TAggEvent>(string aggregateId)
			where TAggEvent : Event;
	}
}