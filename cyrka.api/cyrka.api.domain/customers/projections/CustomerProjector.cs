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
		public CustomerProjector(CustomerProjection customerProjection, IEnumerable<IProjectionOfEvent<CustomerEventData, CustomerFullView>> eventProjections)
		{
			_customerProjection = customerProjection;
			_eventProjections = eventProjections;
		}

		public async Task Apply(Event eventToApply)
		{
			var customerEventData = eventToApply.EventData as CustomerEventData;
			if (customerEventData == null)
				return;

			var expectedType = typeof(IProjectionOfEvent<,>)
				.MakeGenericType(eventToApply.EventData.GetType(), typeof(CustomerFullView));
			var eProjection = _eventProjections
				.FirstOrDefault(p => expectedType.IsInstanceOfType(p));

			if (eProjection == null)
				return;

			var customerViewExisted = _customerProjection.GetCustomerFull(customerEventData.AggregateId);
			var customerViewProjected = eProjection.MakeProjection(customerEventData, customerViewExisted);

			if (customerViewProjected != default(CustomerFullView))
				await _customerProjection.StoreAsync(customerViewProjected);
			else
				await _customerProjection.RemoveAsync(customerEventData.AggregateId);
		}

		private readonly CustomerProjection _customerProjection;
		private readonly IEnumerable<IProjectionOfEvent<CustomerEventData, CustomerFullView>> _eventProjections;
	}
}
