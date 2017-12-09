using System;

namespace cyrka.api.domain.customers.register
{
	public class CustomerRegisterHandler
	{
		public Object[] Handle(CustomerRegister command)
		{
			var customerRegistered = new CustomerRegistered(command.Name, command.Description);
			return new[] { customerRegistered };
		}
	}
}
