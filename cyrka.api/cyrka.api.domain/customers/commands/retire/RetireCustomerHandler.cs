using System.Threading.Tasks;
using cyrka.api.common.commands;
using cyrka.api.common.events;

namespace cyrka.api.domain.customers.commands.retire
{
	public class RetireCustomerHandler : IAggregateCommandHandler<RetireCustomer, CustomerAggregate>
	{
		public Task<EventData[]> Handle(RetireCustomer command, CustomerAggregate aggregate)
		{
			var eventData = new CustomerRetired(aggregate.Id);
			return Task.FromResult<EventData[]>(new[] { eventData });
		}
	}
}
