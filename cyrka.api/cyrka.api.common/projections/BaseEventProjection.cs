using cyrka.api.common.events;
using cyrka.api.common.projections.results;

namespace cyrka.api.common.projections
{
	public abstract class BaseEventProjection<TEventData, TView> : IProjectionOfEvent<TView>
		where TView : IView
		where TEventData : EventData
	{
		public bool CanProject(EventData eventData) => eventData is TEventData;

		public IProjectionResult<TView> Project(EventData eventData, TView source)
		{
			var concreteEventData = eventData as TEventData;
			return concreteEventData == null ? new EmptyProjectionResult<TView>() : Project(concreteEventData, source);
		}

		protected abstract IProjectionResult<TView> Project(TEventData eventData, TView source);
	}
}
