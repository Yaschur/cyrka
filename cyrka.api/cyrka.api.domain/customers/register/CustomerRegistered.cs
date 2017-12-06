namespace cyrka.api.domain.customers.register
{
	public class CustomerRegistered
	{
		public readonly string Id;
		public readonly string Name;
		public readonly string Description;

		public CustomerRegistered(string id, string name, string description)
		{
			Id = id;
			Name = name;
			Description = description;
		}
	}
}
