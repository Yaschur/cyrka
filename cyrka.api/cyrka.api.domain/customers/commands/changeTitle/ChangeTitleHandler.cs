using System.Threading.Tasks;
using cyrka.api.common.commands;
using cyrka.api.common.events;

namespace cyrka.api.domain.customers.commands.changeTitle
{
	public class ChangeTitleHandler : IAggregateCommandHandler<ChangeTitle, CustomerAggregate>
	{
		public Task<EventData[]> Handle(ChangeTitle command, CustomerAggregate aggregate)
		{
			var eventData = new TitleChanged(aggregate.Id, command.TitleId, command.Name, command.NumberOfSeries, command.Description);
			return Task.FromResult<EventData[]>(new[] { eventData });
		}
	}
}
