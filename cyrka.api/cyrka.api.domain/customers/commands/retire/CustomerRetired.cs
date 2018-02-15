namespace cyrka.api.domain.customers.commands.retire
{
	public class CustomerRetired : CustomerEventData
	{
		public CustomerRetired(string id)
			: base(id)
		{ }
	}
}
