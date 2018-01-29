namespace cyrka.api.domain.customers.commands.removeTitle
{
	public class RemoveTitle
	{
		public readonly string CustomerId;
		public readonly string TitleId;

		public RemoveTitle(string customerId, string titleId)
		{
			CustomerId = customerId;
			TitleId = titleId;
		}
	}
}
