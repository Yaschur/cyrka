namespace cyrka.api.domain.customers.commands.register
{
	public class CustomerRegister
	{
		public readonly string Name;
		public readonly string Description;

		public CustomerRegister(string name, string description)
		{
			Name = name;
			Description = description;
		}
	}
}
