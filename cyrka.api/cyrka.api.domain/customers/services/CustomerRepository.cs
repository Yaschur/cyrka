using System.Threading.Tasks;

namespace cyrka.api.domain.customers.services
{
	public class CustomerRepository : ICustomerRepository
	{
		public CustomerRepository(IEventStore eventStore)
		{
			_eventStore = eventStore;
		}

		public async Task<CustomerAggregate> GetById(string customerId)
		{
			var customerEvents = await _eventStore.FindAllByAggregateIdOf(nameof(CustomerAggregate), customerId);
			var customer = new CustomerAggregate(customerEvents);

			return customer;
		}

		public async Task Save(CustomerAggregate customer)
		{
			var unpublishedEvents = customer.ExtractUnpublishedEvents();
			foreach (var unEvent in unpublishedEvents) await _eventStore.Store(unEvent);
			customer.ResetUnpublishedEvents();
		}

		private readonly IEventStore _eventStore;
	}
}
