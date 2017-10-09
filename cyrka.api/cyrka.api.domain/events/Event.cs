namespace cyrka.api.domain.events
{
	public class Event
	{
		public Event(long version, string aggregateType, string aggregateId, string data)
		{
			Version = version;
			AggregateType = aggregateType;
			AggregateId = aggregateId;
			Data = data;
		}

		public long Version { get; }

		public string AggregateType { get; }

		public string AggregateId { get; }

		public string Data { get; }
	}
}