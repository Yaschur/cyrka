namespace cyrka.api.domain.projects.commands.setCustomer
{
	public class CustomerSet : ProjectEventData
	{
		public readonly string CustomerId;
		public readonly string CustomerName;

		public CustomerSet(string projectId, string customerId, string customerName)
			: base(projectId)
		{
			CustomerId = customerId;
			CustomerName = customerName;
		}
	}
}
