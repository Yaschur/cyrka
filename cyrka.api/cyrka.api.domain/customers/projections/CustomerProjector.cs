using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cyrka.api.common.events;
using cyrka.api.common.projections;
using cyrka.api.domain.customers.commands;
using cyrka.api.domain.customers.commands.register;
using cyrka.api.domain.customers.projections.viewModels;

namespace cyrka.api.domain.customers.projections
{
	public class CustomerProjector
	{
		public CustomerProjector(
			IProjectionStore<CustomerFullView> _projectionStore,
			IEnumerable<IProjectionOfEvent<CustomerFullView>> eventProjections
		)
		{
			this._projectionStore = _projectionStore;
			_eventProjections = eventProjections
				.ToArray();
		}

		public async Task Apply(Event eventToApply)
		{
			var eProjection = _eventProjections
				.FirstOrDefault(p => p.CanProject(eventToApply.EventData));

			if (eProjection == null)
				return;

			var customerViewExisted = _projectionStore.GetById(eventToApply.EventData.AggregateId);
			await eProjection
				.Project(eventToApply.EventData, customerViewExisted)
				.AccomplishAsync(_projectionStore);
		}

		private readonly IProjectionStore<CustomerFullView> _projectionStore;
		private readonly IProjectionOfEvent<CustomerFullView>[] _eventProjections;
	}
}
