using System.Threading.Tasks;
using cyrka.api.common.commands;
using cyrka.api.common.events;

namespace cyrka.api.domain.customers.commands.removeTitle
{
	public class RemoveTitleHandler : IAggregateCommandHandler<RemoveTitle, CustomerAggregate>
	{
		public Task<EventData[]> Handle(RemoveTitle command, CustomerAggregate aggregate)
		{
			var eventData = new TitleRemoved(aggregate.Id, command.TitleId);
			return Task.FromResult<EventData[]>(new[] { eventData });
		}
	}
}
