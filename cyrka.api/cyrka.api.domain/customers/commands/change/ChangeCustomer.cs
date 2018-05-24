namespace cyrka.api.domain.customers.commands.change
{
	public class ChangeCustomer
	{
		public readonly string Name;
		public readonly string Description;

		public ChangeCustomer(string name, string description)
		{
			Name = name;
			Description = description;
		}
	}
}
