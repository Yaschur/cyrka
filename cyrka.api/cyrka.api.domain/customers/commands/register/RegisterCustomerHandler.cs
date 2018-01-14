using System;
using cyrka.api.common.events;

namespace cyrka.api.domain.customers.commands.register
{
	public class RegisterCustomerHandler
	{
		public EventData[] Handle(RegisterCustomer command)
		{
			var id = Guid.NewGuid().ToString();
			var customerRegistered = new CustomerRegistered(id, command.Name, command.Description);
			return new[] { customerRegistered };
		}
	}
}
