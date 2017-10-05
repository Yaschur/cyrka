namespace cyrka.api.domain.customers
{
	public class Feature : Product
	{
		public Feature(string id, string name, string description)
			: base(id, name, description, 1)
		{ }
	}
}