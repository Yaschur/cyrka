using cyrka.api.common.events;

namespace cyrka.api.domain.customers.commands.change
{
	public class ChangeCustomerHandler
	{
		public EventData[] Handle(ChangeCustomer command)
		{
			var customerChanged = new CustomerChanged(command.CustomerId, command.Name, command.Description);
			return new[] { customerChanged };
		}
	}
}
