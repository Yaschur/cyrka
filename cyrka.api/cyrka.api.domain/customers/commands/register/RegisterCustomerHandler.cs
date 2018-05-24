using System;
using System.Threading.Tasks;
using cyrka.api.common.commands;
using cyrka.api.common.events;

namespace cyrka.api.domain.customers.commands.register
{
	public class RegisterCustomerHandler : IAggregateCommandHandler<RegisterCustomer, CustomerAggregate>
	{
		public Task<EventData[]> Handle(RegisterCustomer command, CustomerAggregate aggregate)
		{
			var id = Guid.NewGuid().ToString();
			var eventData = new CustomerRegistered(id, command.Name, command.Description);
			return Task.FromResult<EventData[]>(new[] { eventData });
		}
	}
}
