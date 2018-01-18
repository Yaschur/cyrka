namespace cyrka.api.common.events
{
	public abstract class EventData
	{
		public abstract string AggregateType { get; }
		public abstract string AggregateId { get; }
	}
}
