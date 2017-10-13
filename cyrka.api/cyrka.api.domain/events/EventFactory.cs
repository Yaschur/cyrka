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
			EventDataSerializer = eventDataSerializer;
		}

		public IEventDataSerializer EventDataSerializer { get; }

		public Event Create(EventDto eventDto)
		{
			return _aggregateEventFactories.ContainsKey(eventDto.AggregateType) ?
				_aggregateEventFactories[eventDto.AggregateType].Create(eventDto, EventDataSerializer) : null;
		}

		private readonly IDictionary<string, IAggregateEventFactory> _aggregateEventFactories;
	}
}
