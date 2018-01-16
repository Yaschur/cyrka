using System.Linq;
using System.Threading.Tasks;
using cyrka.api.common.events;

namespace cyrka.api.domain.customers
{
	public class CustomerAggregateRepository
	{
		public CustomerAggregateRepository(IEventStore eventStore)
		{
			_eventStore = eventStore;
		}

		public async Task<CustomerAggregate> GetById(string customerId)
		{
			var aggEventDatas = (await _eventStore
				.FindAllOfDataType<CustomerEventData>(ced => ced.CustomerId == customerId))
				.Select(e => e.EventData as CustomerEventData)
				.ToArray();

			if (aggEventDatas.Length == 0)
				return null;

			var customer = new CustomerAggregate();
			customer.ApplyEvents(aggEventDatas);

			return customer;
		}

		private readonly IEventStore _eventStore;
	}
}
