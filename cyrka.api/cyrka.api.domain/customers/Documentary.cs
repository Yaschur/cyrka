namespace cyrka.api.domain.customers
{
	public class Documentary : Product
	{
		protected Documentary(string id, string name, string description)
			: base(id, name, description, 1)
		{ }
	}
}