namespace cyrka.api.domain.customers.commands.register
{
	public class CustomerRegistered : CustomerEventData
	{
		public readonly string Name;
		public readonly string Description;

		public CustomerRegistered(string id, string name, string description)
			: base(id)
		{
			Name = name;
			Description = description;
		}
	}
}
