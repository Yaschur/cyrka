using System.Linq;
using System.Threading.Tasks;
using cyrka.api.common.commands;
using cyrka.api.common.events;

namespace cyrka.api.domain.customers
{
	public class CustomerAggregateRepository : IAggregateRepository<CustomerAggregate>
	{
		public CustomerAggregateRepository(IEventStore eventStore)
		{
			_eventStore = eventStore;
		}

		public async Task<CustomerAggregate> GetById(string customerId)
		{
			var aggEventDatas = (await _eventStore
				.FindAllOfAggregateById<CustomerAggregate>(customerId))
				.Select(e => e.EventData)
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
