namespace cyrka.api.domain.customers.commands.change
{
	public class ChangeCustomer
	{
		public readonly string CustomerId;
		public readonly string Name;
		public readonly string Description;

		public ChangeCustomer(string customerId, string name, string description)
		{
			CustomerId = customerId;
			Name = name;
			Description = description;
		}
	}
}
