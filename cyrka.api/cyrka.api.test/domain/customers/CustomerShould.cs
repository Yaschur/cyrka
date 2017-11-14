using System;
using cyrka.api.domain.customers;
using cyrka.api.domain.customers.events;
using NUnit.Framework;

namespace cyrka.api.test.domain.customers
{
	[TestFixture]
	public class CustomerShould
	{
		[Test]
		public void HaveNoUnpublishedEventsIfNew()
		{
			var sut = new CustomerAggregate();

			Assert.AreEqual(0, sut.ExtractUnpublishedEvents().Length);
		}

		[Test]
		public void ThrowOnRegisterIfCreatedByEvents()
		{
			var dto = new CustomerDto { Id = "abc", Description = "", Name = "abc" };
			var events = new[] { new CustomerRegistered(1, dto) };
			var sut = new CustomerAggregate(events);

			Assert.Throws<InvalidOperationException>(() => sut.Register("yxz", "yxz", ""));
		}

		[Test]
		public void ExtractUnpublishedEvents()
		{
			var sut = new CustomerAggregate();
			sut.Register("abc", "abd", "");

			var uevents = sut.ExtractUnpublishedEvents();

			Assert.IsNotEmpty(uevents);
		}

		[Test]
		public void ClearUnpublishedEvents()
		{
			var sut = new CustomerAggregate();
			sut.Register("abc", "abd", "");

			sut.ResetUnpublishedEvents();

			Assert.IsEmpty(sut.ExtractUnpublishedEvents());
		}
	}
}
