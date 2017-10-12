using System.Collections.Generic;
using System.Linq;

namespace cyrka.api.domain.events
{
	public class EventFactory
	{
		public EventFactory(IEventDataSerializer eventDataSerializer, IEnumerable<IAggregateEventFactory> aggregateEventFactories)
		{
			_aggregateEventFactories = (aggregateEventFactories ?? Enumerable.Empty<IAggregateEventFactory>())
				.ToDictionary(f => f.AggregateType);
			_eventDataSerializer = eventDataSerializer;
		}

		public Event Create(EventDto eventDto)
		{
			return _aggregateEventFactories.ContainsKey(eventDto.AggregateType) ?
				_aggregateEventFactories[eventDto.AggregateType].Create(eventDto, _eventDataSerializer) : null;
		}

		private readonly IDictionary<string, IAggregateEventFactory> _aggregateEventFactories;
		private readonly IEventDataSerializer _eventDataSerializer;
	}
}
