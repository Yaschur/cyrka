using System.Threading.Tasks;
using cyrka.api.common.commands;
using cyrka.api.common.events;

namespace cyrka.api.domain.customers.commands.change
{
	public class ChangeCustomerHandler : IAggregateCommandHandler<ChangeCustomer, CustomerAggregate>
	{
		public Task<EventData[]> Handle(ChangeCustomer command, CustomerAggregate aggregate)
		{
			var eventData = new CustomerChanged(aggregate.Id, command.Name, command.Description);
			return Task.FromResult<EventData[]>(new[] { eventData });
		}
	}
}
