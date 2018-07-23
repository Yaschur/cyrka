using System.Collections.Generic;
using cyrka.api.common.events;
using cyrka.api.common.projections;
using cyrka.api.common.projections.results;
using cyrka.api.domain.customers.commands.change;
using cyrka.api.domain.customers.commands.register;
using cyrka.api.domain.customers.projections.viewModels;

namespace cyrka.api.domain.customers.projections.eventProjections
{
	public class CustomerChangedProjection : BaseEventProjection<CustomerChanged, CustomerFullView>
	{
		protected override IProjectionResult<CustomerFullView> Project(CustomerChanged eventData, CustomerFullView target)
		{
			target.Name = eventData.Name;
			target.Description = eventData.Description;
			return new StoreProjectionResult<CustomerFullView>(target);
		}
	}
}
