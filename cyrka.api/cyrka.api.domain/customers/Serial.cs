namespace cyrka.api.domain.customers
{
	public class Serial : Product
	{
		protected Serial(string id, string name, string description)
			: base(id, name, description, 1)
		{ }

		public Episode[] Episodes { get; private set; }
	}
}