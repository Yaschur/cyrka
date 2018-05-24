using System;
using System.Threading.Tasks;
using cyrka.api.common.commands;
using cyrka.api.common.events;

namespace cyrka.api.domain.customers.commands.registerTitle
{
	public class RegisterTitleHandler : IAggregateCommandHandler<RegisterTitle, CustomerAggregate>
	{
		public Task<EventData[]> Handle(RegisterTitle command, CustomerAggregate aggregate)
		{
			var id = Guid.NewGuid().ToString();
			var eventData = new TitleRegistered(aggregate.Id, id, command.Name, command.NumberOfSeries, command.Description);
			return Task.FromResult<EventData[]>(new[] { eventData });
		}
	}
}
