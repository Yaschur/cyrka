using System.Collections.Generic;
using System.Linq;
using cyrka.api.domain.customers.commands.register;
using cyrka.api.domain.customers.commands.registerTitle;

namespace cyrka.api.domain.customers
{
	public class CustomerAggregate
	{
		public string Id { get; private set; }
		public string Name { get; private set; }
		public string Description { get; private set; }
		public Title[] Titles { get; private set; }

		public CustomerAggregate()
		{
			Titles = new Title[0];
		}

		public void ApplyEvents(IEnumerable<CustomerEventData> eventDatas)
		{
			if (eventDatas == null)
				return;

			foreach (var eventData in eventDatas)
			{
				switch (eventData)
				{
					case CustomerRegistered customerRegistered:
						ApplyEvent(customerRegistered);
						break;
					case TitleRegistered titleRegistered:
						ApplyEvent(titleRegistered);
						break;
				}
			}
		}

		private void ApplyEvent(CustomerRegistered customerEvent)
		{
			// if (Id == null)
			// 	return;
			Id = customerEvent.CustomerId;
			Name = customerEvent.Name;
			Description = customerEvent.Description;
		}
		private void ApplyEvent(TitleRegistered customerEvent)
		{
			var newTitle = new Title
			{
				CustomerId = customerEvent.CustomerId,
				Id = customerEvent.TitleId,
				Name = customerEvent.Name,
				NumberOfSeries = customerEvent.NumberOfSeries,
				Description = customerEvent.Description
			};

			var list = Titles
				.ToList();
			list.Add(newTitle);
			Titles = list.ToArray();
		}
	}
}
