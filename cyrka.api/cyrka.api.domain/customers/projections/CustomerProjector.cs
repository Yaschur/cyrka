using System.Threading.Tasks;
using cyrka.api.common.events;
using cyrka.api.domain.customers.commands.register;

namespace cyrka.api.domain.customers.projections
{
	public class CustomerProjector
	{
		public CustomerProjector(CustomerProjection customerProjection) =>
			_customerProjection = customerProjection;

		public async Task Apply(Event @event) {
			// TODO: What if manufacture small services like view -> eventData -> view, load them by interface


		}

		private readonly CustomerProjection _customerProjection;
	}
}
