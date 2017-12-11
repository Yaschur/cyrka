using System;
using cyrka.api.domain.events;

namespace cyrka.api.domain.customers.register
{
	public class CustomerRegisterHandler
	{
		public EventData[] Handle(CustomerRegister command)
		{
			var customerRegistered = new CustomerRegistered(command.Name, command.Description);
			return new[] { customerRegistered };
		}
	}
}
