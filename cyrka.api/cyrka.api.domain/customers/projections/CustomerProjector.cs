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
			IReadProjection<CustomerFullView> projectionReader,
			IWriteProjection<CustomerFullView> projectionWriter,
			IEnumerable<IProjectionOfEvent<CustomerFullView>> eventProjections
		)
		{
			_projectionReader = projectionReader;
			_projectionWriter = projectionWriter;
			_eventProjections = eventProjections;
		}

		public async Task Apply(Event eventToApply)
		{
			var eProjection = _eventProjections
				.FirstOrDefault(p => p.CanProject(eventToApply.EventData));

			if (eProjection == null)
				return;

			var customerViewExisted = _projectionReader.GetById(eventToApply.EventData.AggregateId);
			await eProjection
				.Project(eventToApply.EventData, customerViewExisted)
				.AccomplishAsync(_projectionWriter);


		}

		private readonly IReadProjection<CustomerFullView> _projectionReader;
		private readonly IWriteProjection<CustomerFullView> _projectionWriter;
		private readonly IEnumerable<IProjectionOfEvent<CustomerFullView>> _eventProjections;
	}
}
