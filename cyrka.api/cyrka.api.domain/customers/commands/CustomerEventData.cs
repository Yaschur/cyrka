using cyrka.api.common.events;

namespace cyrka.api.domain.customers
{
	public class CustomerEventData : EventData
	{
		public readonly string CustomerId;

		public CustomerEventData(string customerId)
		{
			CustomerId = customerId;
		}
	}
}
