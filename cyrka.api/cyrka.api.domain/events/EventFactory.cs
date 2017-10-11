using System.Collections.Generic;
using System.Linq;

namespace cyrka.api.domain.events
{
	public class EventFactory
	{
		public EventFactory(IEnumerable<IAggregateEventFactory> aggregateEventFactories)
		{
			_aggregateEventFactories = (aggregateEventFactories ?? Enumerable.Empty<IAggregateEventFactory>())
				.ToDictionary(f => f.AggregateType);
		}

		public Event Create(EventDto eventDto)
		{
			return _aggregateEventFactories[eventDto.AggregateType]?.Create(eventDto);
		}

		private readonly IDictionary<string, IAggregateEventFactory> _aggregateEventFactories;
	}
}
