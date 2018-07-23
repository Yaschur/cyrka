using cyrka.api.common.events;

namespace cyrka.api.common.projections
{
	public interface IProjectionOfEvent<TView>
		where TView : IView
	{
		bool CanProject<TEventData>(TEventData eventData)
			where TEventData : EventData;
		IProjectionResult<TView> Project(EventData eventData, TView source);
	}

	public interface IProjectionOfEvent<TEventData, TView> : IProjectionOfEvent<TView>
		where TView : IView
	{
		IProjectionResult<TView> Project(TEventData eventData, TView source);
	}
}
