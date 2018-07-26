using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cyrka.api.common.events;
using cyrka.api.common.projections;

namespace cyrka.api.common.projections
{
	public class Projectionist
	{
		public Projectionist(IEnumerable<IProjector> projectors)
		{
			_projectors = projectors
				.ToArray();
		}

		public async Task Apply(Event incomingEvent)
		{
			foreach (var projector in _projectors)
			{
				await projector.Apply(incomingEvent);
			}
		}

		private IProjector[] _projectors;
	}
}
