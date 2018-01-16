using System;
using System.Linq.Expressions;
using cyrka.api.common.queries;
using cyrka.api.domain.customers.commands.register;
using cyrka.api.domain.customers.commands.registerTitle;

namespace cyrka.api.domain.customers.queries
{
	public class CustomerPlainRegistrator
	{
		public void RegisterIn(QueryEventProcessor processor)
		{
			processor.RegisterEventProcessing<CustomerRegistered, CustomerPlain>(UpdateByEventData, IdFilterByEventData);
			processor.RegisterEventProcessing<TitleRegistered, CustomerPlain>(UpdateByEventData, IdFilterByEventData);
		}

		public Expression<Func<CustomerPlain, bool>> IdFilterByEventData(CustomerEventData eventData)
		{
			return c => c.Id == eventData.CustomerId;
		}

		public CustomerPlain UpdateByEventData(CustomerRegistered eventData, CustomerPlain source)
		{
			return new CustomerPlain
			{
				Id = eventData.CustomerId,
				Name = eventData.Name,
				Description = eventData.Description
			};
		}

		public CustomerPlain UpdateByEventData(TitleRegistered eventData, CustomerPlain source)
		{
			source.Titles.Add(new TitlePlain
			{
				Id = eventData.TitleId,
				Name = eventData.Name,
				NumberOfSeries = eventData.NumberOfSeries,
				Description = eventData.Description
			});

			return source;
		}
	}
}
