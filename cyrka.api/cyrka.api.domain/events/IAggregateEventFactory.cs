namespace cyrka.api.domain.events
{
	public interface IAggregateEventFactory
	{
		string AggregateType { get; }

		Event Create(EventDto eventDto, IEventDataSerializer dataSerializer);
	}
}
