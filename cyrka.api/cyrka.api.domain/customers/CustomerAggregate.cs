using System.Collections.Generic;
using System.Linq;
using cyrka.api.common.events;
using cyrka.api.domain.customers.commands.change;
using cyrka.api.domain.customers.commands.changeTitle;
using cyrka.api.domain.customers.commands.register;
using cyrka.api.domain.customers.commands.registerTitle;
using cyrka.api.domain.customers.commands.removeTitle;
using cyrka.api.domain.customers.commands.retire;

namespace cyrka.api.domain.customers
{
	public class CustomerAggregate
	{
		public string Id { get; private set; }
		public string Name { get; private set; }
		public string Description { get; private set; }
		public Title[] Titles { get; private set; }
		public bool IsRetired { get; private set; }

		public CustomerAggregate()
		{
			Titles = new Title[0];
			IsRetired = false;
		}

		public void ApplyEvents(IEnumerable<EventData> eventDatas)
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
					case CustomerChanged customerChanged:
						ApplyEvent(customerChanged);
						break;
					case TitleRegistered titleRegistered:
						ApplyEvent(titleRegistered);
						break;
					case TitleChanged titleChanged:
						ApplyEvent(titleChanged);
						break;
					case CustomerRetired customerRetired:
						ApplyEvent(customerRetired);
						break;
				}
			}
		}

		private void ApplyEvent(CustomerRegistered customerEvent)
		{
			// if (Id == null)
			// 	return;
			Id = customerEvent.AggregateId;
			Name = customerEvent.Name;
			Description = customerEvent.Description;
		}

		private void ApplyEvent(CustomerChanged customerEvent)
		{
			Name = customerEvent.Name;
			Description = customerEvent.Description;
		}

		private void ApplyEvent(TitleRegistered customerEvent)
		{
			var newTitle = new Title
			{
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

		private void ApplyEvent(TitleChanged customerEvent)
		{
			var exTitle = Titles
				.FirstOrDefault(t => t.Id == customerEvent.TitleId);
			if (exTitle == null)
				return;

			exTitle.Name = customerEvent.Name;
			exTitle.NumberOfSeries = customerEvent.NumberOfSeries;
			exTitle.Description = customerEvent.Description;

			var list = Titles
				.ToList();
			list.RemoveAll(t => t.Id == exTitle.Id);
			list.Add(exTitle);
			Titles = list.ToArray();
		}

		private void ApplyEvent(CustomerRetired customerEvent)
		{
			IsRetired = true;
		}

		private void ApplyEvent(TitleRemoved customerEvent)
		{
			var exTitle = Titles
				.FirstOrDefault(t => t.Id == customerEvent.TitleId);
			if (exTitle == null)
				return;
			var list = Titles
				.ToList();
			list.RemoveAll(t => t.Id == exTitle.Id);
			Titles = list.ToArray();
		}
	}
}
