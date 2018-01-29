namespace cyrka.api.domain.customers.commands.registerTitle
{
	public class RegisterTitle
	{
		public readonly string CustomerId;
		public readonly string Name;
		public readonly int NumberOfSeries;
		public readonly string Description;

		public RegisterTitle(string customerId, string name, int numberOfSeries, string description)
		{
			CustomerId = customerId;
			Name = name;
			NumberOfSeries = numberOfSeries;
			Description = description;
		}
	}
}
