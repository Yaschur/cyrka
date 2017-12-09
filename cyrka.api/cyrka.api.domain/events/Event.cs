using System;

namespace cyrka.api.domain.events
{
	public class Event
	{
		public readonly ulong Id;
		public readonly DateTime CreatedAt;
		public readonly Object EventData;

		public Event(ulong id, DateTime createdAt, Object eventData)
		{
			Id = id;
			CreatedAt = createdAt;
			EventData = eventData;
		}
	}
}
