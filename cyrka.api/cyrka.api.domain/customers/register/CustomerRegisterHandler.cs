using System;

namespace cyrka.api.domain.customers.register
{
	public class CustomerRegisterHandler
	{
		public CustomerRegistered Handle(CustomerRegister command)
		{
			var customerRegistered = new CustomerRegistered(Guid.NewGuid().ToString(), command.Name, command.Description);
			return customerRegistered;
		}
	}
}
