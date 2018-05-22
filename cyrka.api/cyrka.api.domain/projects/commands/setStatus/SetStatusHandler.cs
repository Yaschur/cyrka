using System.Threading.Tasks;
using cyrka.api.common.commands;
using cyrka.api.common.events;

namespace cyrka.api.domain.projects.commands.setStatus
{
	public class SetStatusHandler : IAggregateCommandHandler<SetStatus, ProjectAggregate>
	{
		public Task<EventData[]> Handle(SetStatus command, ProjectAggregate aggregate)
		{
			var eventData = new StatusSet(aggregate.State.ProjectId, command.Status);

			return Task.FromResult<EventData[]>(new[] { eventData });
		}
	}
}
