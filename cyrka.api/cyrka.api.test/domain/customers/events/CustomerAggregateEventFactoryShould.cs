using System;
using cyrka.api.domain.customers;
using cyrka.api.domain.customers.events;
using cyrka.api.domain.events;
using FakeItEasy;
using NUnit.Framework;

namespace cyrka.api.test.domain.customers.events
{
	[TestFixture]
	public class CustomerAggregateEventFactoryShould
	{
		CustomerEventFactory sut;

		[SetUp]
		public void Setup()
		{
			sut = new CustomerEventFactory();
		}

		[Test]
		public void ThrowsOnCreateUnknownEvent()
		{
			var eventType = TestContext.CurrentContext.Random.GetString();
			var eventDto = new EventDto(
				1,
				DateTime.UtcNow,
				eventType,
				nameof(CustomerAggregate),
				"someId",
				"someData"
			);

			Assert.Throws<ArgumentException>(() => sut.Create(eventDto, A.Fake<IEventDataSerializer>()));
		}

		[Test]
		public void CreateCustomerRegistered()
		{
			var eventType = CustomerRegistered.EventTypeName;
			var eventDto = new EventDto(
				1,
				DateTime.UtcNow,
				eventType,
				nameof(CustomerAggregate),
				"someId",
				"someData"
			);

			var result = sut.Create(eventDto, A.Fake<IEventDataSerializer>());

			Assert.IsInstanceOf<CustomerRegistered>(result);
			Assert.AreEqual(1, ((CustomerRegistered)result).EventId);
		}
	}
}
