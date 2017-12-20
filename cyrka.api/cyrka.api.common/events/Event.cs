using System;

namespace cyrka.api.common.events
{
	public class Event
	{
		public readonly ulong Id;
		public readonly DateTime CreatedAt;
		public readonly EventData EventData;

		public Event(ulong id, DateTime createdAt, EventData eventData)
		{
			Id = id;
			CreatedAt = createdAt;
			EventData = eventData;
		}
	}
}
