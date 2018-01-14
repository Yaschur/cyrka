namespace cyrka.api.domain.customers.commands.register
{
	public class RegisterCustomer
	{
		public readonly string Name;
		public readonly string Description;

		public RegisterCustomer(string name, string description)
		{
			Name = name;
			Description = description;
		}
	}
}
