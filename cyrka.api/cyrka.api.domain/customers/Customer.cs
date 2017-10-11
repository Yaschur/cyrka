using System;
using System.Collections.Generic;
using cyrka.api.domain.customers.events;
using cyrka.api.domain.events;

namespace cyrka.api.domain.customers
{
	public class Customer
	{
		public Customer()
		{
			_unpublishedEvents = new List<Event>();
		}

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

		public Event[] FindAllUnpublishedEvents()
		{
			return _unpublishedEvents.ToArray();
		}

		private readonly List<Event> _unpublishedEvents;
		private CustomerDto _customerDto { get; set; }
	}
}
