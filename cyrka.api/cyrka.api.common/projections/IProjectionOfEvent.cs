using cyrka.api.common.events;

namespace cyrka.api.common.projections
{
	public interface IProjectionOfEvent<TView>
	{
		bool CanProject<TEventData>(TEventData eventData)
			where TEventData : EventData;
		TView MakeProjection(EventData eventData, TView source);
	}

	public interface IProjectionOfEvent<TEventData, TView> : IProjectionOfEvent<TView>
	{
		TView MakeProjection(TEventData eventData, TView source);
	}
}
