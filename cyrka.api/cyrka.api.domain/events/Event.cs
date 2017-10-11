namespace cyrka.api.domain.events
{
	public abstract class Event
	{
		public abstract EventDto GetEventDto(IEventDataSerializer serializer);
	}
}
