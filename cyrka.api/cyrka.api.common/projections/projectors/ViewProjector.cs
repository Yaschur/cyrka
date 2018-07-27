using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cyrka.api.common.events;

namespace cyrka.api.common.projections.projectors
{
	public class ViewProjector<TView> : IProjector
		where TView : IView
	{
		public ViewProjector(
			IProjectionStore<TView> projectionStore,
			IEnumerable<IProjectionOfEvent<TView>> eventProjections
		)
		{
			_projectionStore = projectionStore;
			_eventProjections = eventProjections
				.ToArray();
		}

		public async Task Apply(Event eventToApply)
		{
			var eProjection = _eventProjections
				.FirstOrDefault(p => p.CanProject(eventToApply.EventData));

			if (eProjection == null)
				return;

			var viewExisted = _projectionStore.GetById(eventToApply.EventData.AggregateId);
			await eProjection
				.Project(eventToApply.EventData, viewExisted)
				.AccomplishAsync(_projectionStore);
		}

		public async Task Reset()
		{
			await _projectionStore.ClearAsync();
		}

		private readonly IProjectionStore<TView> _projectionStore;
		private readonly IProjectionOfEvent<TView>[] _eventProjections;
	}
}
