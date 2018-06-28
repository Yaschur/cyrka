using cyrka.api.common.events;

namespace cyrka.api.common.projections
{
	public interface IProjectionOfEvent<in TEventData, TView>
	{
		TView MakeProjection(TEventData eventData, TView source);
	}
}
