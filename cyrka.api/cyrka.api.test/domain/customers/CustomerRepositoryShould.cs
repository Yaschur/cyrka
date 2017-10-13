using System.Threading.Tasks;
using cyrka.api.domain;
using cyrka.api.domain.customers;
using cyrka.api.domain.customers.events;
using cyrka.api.domain.customers.services;
using cyrka.api.domain.events;
using FakeItEasy;
using NUnit.Framework;

namespace cyrka.api.test.domain.customers
{
	[TestFixture]
	public class CustomerRepositoryShould
	{
		CustomerRepository sut;

		IEventStore eventStore;

		[SetUp]
		public void Setup()
		{
			eventStore = A.Fake<IEventStore>();
			sut = new CustomerRepository(eventStore);
		}

		[Test]
		public async Task TryRestoreCustomerByEventsFromEventStore()
		{
			await sut.GetById("abc");

			A.CallTo(() => eventStore.FindAllByAggregateIdOf(nameof(Customer), "abc"))
				.MustHaveHappened();
		}

		[Test]
		public async Task TryStoreUnpublishedEventsToEventStore()
		{
			var customer = new Customer();
			customer.Register("abc", "abc", "");

			await sut.Save(customer);

			A.CallTo(() => eventStore.Store(A<Event>.That.IsInstanceOf(typeof(CustomerRegistered))))
				.MustHaveHappened();
			Assert.AreEqual(0, customer.ExtractUnpublishedEvents().Length);
		}
	}
}
