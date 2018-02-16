namespace cyrka.api.domain.projects.commands.setCustomer
{
	public class SetCustomer
	{
		public readonly string ProjectId;
		public readonly string CustomerId;
		public readonly string CustomerName;

		public SetCustomer(string projectId, string customerId, string customerName)
		{
			ProjectId = projectId;
			CustomerId = customerId;
			CustomerName = customerName;
		}
	}
}
