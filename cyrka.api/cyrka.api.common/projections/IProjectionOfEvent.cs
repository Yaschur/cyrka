using cyrka.api.common.events;

namespace cyrka.api.common.projections
{
	public interface IProjectionOfEvent<TView>
		where TView : IView
	{
		bool CanProject(EventData eventData);

		IProjectionResult<TView> Project(EventData eventData, TView target);
	}

	// public interface IProjectionOfEvent<TEventData, TView> : IProjectionOfEvent<TView>
	// 	where TView : IView
	// 	where TEventData : EventData
	// {
	// 	IProjectionResult<TView> Project(TEventData eventData, TView source);
	// }
}
