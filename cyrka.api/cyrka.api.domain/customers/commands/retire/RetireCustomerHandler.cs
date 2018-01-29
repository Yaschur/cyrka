using System;
using cyrka.api.common.events;

namespace cyrka.api.domain.customers.commands.retire
{
	public class RetireCustomerHandler
	{
		public EventData[] Handle(RetireCustomer command)
		{
			var customerRetired = new CustomerRetired(command.CustomerId);
			return new[] { customerRetired };
		}
	}
}
