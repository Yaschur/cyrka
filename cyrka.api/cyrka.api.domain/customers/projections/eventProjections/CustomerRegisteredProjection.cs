using System.Collections.Generic;
using cyrka.api.common.events;
using cyrka.api.common.projections;
using cyrka.api.common.projections.results;
using cyrka.api.domain.customers.commands.register;
using cyrka.api.domain.customers.projections.viewModels;

namespace cyrka.api.domain.customers.projections.eventProjections
{
	public class CustomerRegisteredProjection : BaseEventProjection<CustomerRegistered, CustomerFullView>
	{
		protected override IProjectionResult<CustomerFullView> Project(CustomerRegistered eventData, CustomerFullView target)
		{
			var customerView = new CustomerFullView
			{
				Id = eventData.AggregateId,
				Name = eventData.Name,
				Description = eventData.Description,
				TitlesCount = 0,
				Titles = new List<TitleView>(),
			};
			return new StoreProjectionResult<CustomerFullView>(customerView);
		}
	}
}
