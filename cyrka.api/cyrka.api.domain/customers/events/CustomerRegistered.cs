using cyrka.api.domain.events;

namespace cyrka.api.domain.customers.events
{
	public class CustomerRegistered : Event
	{
		public CustomerRegistered(long version, string aggregateType, string aggregateId, string data) : base(version, aggregateType, aggregateId, data)
		{
		}
	}
}