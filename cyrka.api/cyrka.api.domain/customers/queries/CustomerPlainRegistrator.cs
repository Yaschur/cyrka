using System;
using System.Linq.Expressions;
using cyrka.api.common.queries;
using cyrka.api.domain.customers.commands.register;

namespace cyrka.api.domain.customers.queries
{
	public class CustomerPlainRegistrator
	{
		public void RegisterIn(QueryEventProcessor processor)
		{
			processor.RegisterEventProcessing<CustomerRegistered, CustomerPlain>(UpdateByEventData, IdFilterByEventData);
		}

		public Expression<Func<CustomerPlain, bool>> IdFilterByEventData(CustomerRegistered eventData)
		{
			return c => c.Id == eventData.Id;
		}

		public CustomerPlain UpdateByEventData(CustomerRegistered eventData, CustomerPlain source)
		{
			return new CustomerPlain
			{
				Id = eventData.Id,
				Name = eventData.Name,
				Description = eventData.Description
			};
		}
	}
}
