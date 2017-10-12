using System;
using cyrka.api.domain.events;
using cyrka.api.domain.customers;

namespace cyrka.api.domain.customers.events
{
	public class CustomerRegistered : Event
	{
		public const string EventTypeName = "CustomerRegistered";

		public CustomerRegistered(CustomerDto customerDto)
			: this(0, customerDto)
		{
		}

		public CustomerRegistered(ulong eventId, CustomerDto customerDto)
		{
			EventId = eventId;
			Data = customerDto;
		}

		public ulong EventId { get; }

		public CustomerDto Data { get; }

		public override EventDto GetEventDto(IEventDataSerializer serializer)
		{
			return new EventDto(EventId, DateTime.UtcNow, EventTypeName, nameof(Customer), Data.Id, serializer.Serialize(Data));
		}
	}
}
