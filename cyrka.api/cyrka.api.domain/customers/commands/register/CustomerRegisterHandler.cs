using System;
using cyrka.api.domain.events;

namespace cyrka.api.domain.customers.commands.register
{
	public class CustomerRegisterHandler
	{
		public EventData[] Handle(CustomerRegister command)
		{
			var id = Guid.NewGuid().ToString();
			var customerRegistered = new CustomerRegistered(id, command.Name, command.Description);
			return new[] { customerRegistered };
		}
	}
}
