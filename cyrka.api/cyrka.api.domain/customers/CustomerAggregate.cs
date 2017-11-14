using System;
using System.Collections.Generic;
using System.Linq;
using cyrka.api.domain.customers.events;
using cyrka.api.domain.events;

namespace cyrka.api.domain.customers
{
	public class CustomerAggregate
	{
		public CustomerAggregate() => _unpublishedEvents = new List<Event>();

		public CustomerAggregate(Event[] customerEvents) : this() => Replay(customerEvents);
		public void Register(string id, string name, string description)
		{
			if (_customerDto != null)
				throw new InvalidOperationException("Customer is already registered");

			_customerDto = new CustomerDto
			{
				Id = id,
				Name = name,
				Description = description
			};

			_unpublishedEvents.Add(new CustomerRegistered(_customerDto));
		}

		public Event[] ExtractUnpublishedEvents()
		{
			var result = _unpublishedEvents.ToArray();
			return result;
		}

		public void ResetUnpublishedEvents()
		{
			_unpublishedEvents.Clear();
		}

		private readonly List<Event> _unpublishedEvents;

		private CustomerDto _customerDto { get; set; }

		private void Replay(Event[] customerEvents)
		{
			var cEvents = customerEvents ?? new Event[0];
			_customerDto = ((CustomerRegistered)cEvents
				.FirstOrDefault(ce => ce is CustomerRegistered))?
				.Data;
		}
	}
}
