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
		public Projectionist(IEventStore eventStore, IEnumerable<IProjector> projectors)
		{
			_projectors = projectors
				.ToArray();
			_eventStore = eventStore;
		}

		public async Task Start(bool withReset = false)
		{
			// TODO: need stronger protection
			if (_subscription != null)
				return;

			if (withReset)
			{
				foreach (var projector in _projectors)
				{
					await projector.Reset();
				}
			}

			_subscription = _eventStore.AsObservable(withReset)
				.Subscribe(async incomingEvent =>
				{
					foreach (var projector in _projectors)
					{
						await projector.Apply(incomingEvent);
					}
				});
		}

		private readonly IProjector[] _projectors;
		private readonly IEventStore _eventStore;
		private IDisposable _subscription;
	}
}
