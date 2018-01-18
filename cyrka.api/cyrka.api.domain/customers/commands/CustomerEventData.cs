using cyrka.api.common.events;

namespace cyrka.api.domain.customers.commands
{
	public abstract class CustomerEventData : EventData
	{
		public override string AggregateType => nameof(CustomerAggregate);

		public override string AggregateId { get; }

		public CustomerEventData(string customerId)
		{
			AggregateId = customerId;
		}
	}
}
