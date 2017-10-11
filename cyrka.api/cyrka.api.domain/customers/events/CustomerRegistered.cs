using System;
using cyrka.api.domain.events;
using cyrka.api.domain.customers;

namespace cyrka.api.domain.customers.events
{
	public class CustomerRegistered : Event
	{
		const string EventTypeName = "CustomerRegistered";
		const string AggregateTypeName = "Customer";

		public CustomerRegistered(string customerId, CustomerDto customerDto)
			: this(0, customerId, customerDto)
		{
		}

		public CustomerRegistered(ulong eventId, string customerId, CustomerDto customerDto)
		{
			EventId = eventId;
			CustomerId = customerId;
			Data = customerDto;
		}

		public ulong EventId { get; }

		public string CustomerId { get; }

		public CustomerDto Data { get; }

		public override EventDto GetEventDto(IEventDataSerializer serializer)
		{
			return new EventDto(EventId, DateTime.UtcNow, EventTypeName, AggregateTypeName, CustomerId, serializer.Serialize(Data));
		}
	}
}
