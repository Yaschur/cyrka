using cyrka.api.domain.events;
using System;

namespace cyrka.api.domain.customers.events
{
	public class CustomerAggregateEventFactory : IAggregateEventFactory
	{
		public string AggregateType => nameof(Customer);

		public Event Create(EventDto eventDto, IEventDataSerializer dataSerializer)
		{
			switch (eventDto.EventType)
			{
				case CustomerRegistered.EventTypeName:
					return new CustomerRegistered(eventDto.Id, dataSerializer.Deserialize<CustomerDto>(eventDto.Data));
				default:
					throw new ArgumentException("Unknown event data", nameof(eventDto));
			}
		}
	}
}
