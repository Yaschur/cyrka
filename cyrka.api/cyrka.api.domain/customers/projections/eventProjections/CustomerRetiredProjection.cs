using cyrka.api.common.projections;
using cyrka.api.common.projections.results;
using cyrka.api.domain.customers.commands.retire;
using cyrka.api.domain.customers.projections.viewModels;

namespace cyrka.api.domain.customers.projections.eventProjections
{
	public class CustomerRetiredProjection : BaseEventProjection<CustomerRetired, CustomerFullView>
	{
		protected override IProjectionResult<CustomerFullView> Project(CustomerRetired eventData, CustomerFullView target)
		{
			return new RemoveProjectionResult<CustomerFullView>(target);
		}
	}
}
