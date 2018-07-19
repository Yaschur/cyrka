using cyrka.api.common.events;

namespace cyrka.api.common.projections
{
	public interface IProjectionOfEvent<TView>
	{
		bool CanProject<TEventData>(TEventData eventData)
			where TEventData : EventData;
		IProjectionResult Project(EventData eventData, TView source);
	}

	public interface IProjectionOfEvent<TEventData, TView> : IProjectionOfEvent<TView>
	{
		IProjectionResult Project(TEventData eventData, TView source);
	}
}
