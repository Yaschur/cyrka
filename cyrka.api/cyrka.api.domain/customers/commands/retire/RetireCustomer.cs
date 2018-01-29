namespace cyrka.api.domain.customers.commands.retire
{
	public class RetireCustomer
	{
		public readonly string CustomerId;

		public RetireCustomer(string customerId)
		{
			CustomerId = customerId;
		}
	}
}
