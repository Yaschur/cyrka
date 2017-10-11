using System;

namespace cyrka.api.domain.events
{
	public class EventDto
	{
		public EventDto(ulong id, DateTime createdAt, string eventType, string aggregateType, string aggregateId, string data)
		{
			Id = id;
			CreatedAt = createdAt;
			EventType = eventType;
			AggregateType = aggregateType;
			AggregateId = aggregateId;
			Data = data;
		}

		public ulong Id { get; }

		public DateTime CreatedAt { get; }

		public string EventType { get; }

		public string AggregateType { get; }

		public string AggregateId { get; }

		public string Data { get; }
	}
}
