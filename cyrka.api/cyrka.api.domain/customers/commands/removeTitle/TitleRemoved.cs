namespace cyrka.api.domain.customers.commands.removeTitle
{
	public class TitleRemoved : CustomerEventData
	{
		public readonly string TitleId;

		public TitleRemoved(string customerId, string titleId)
			: base(customerId)
		{
			TitleId = titleId;
		}
	}
}
