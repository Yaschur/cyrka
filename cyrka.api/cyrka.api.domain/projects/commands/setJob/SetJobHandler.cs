using System.Threading.Tasks;
using cyrka.api.common.commands;
using cyrka.api.common.events;

namespace cyrka.api.domain.projects.commands.setJob
{
	public class SetJobHandler : IAggregateCommandHandler<SetJob, ProjectAggregate>
	{
		public Task<EventData[]> Handle(SetJob command, ProjectAggregate aggregate)
		{
			var eventData = new JobSet(
				aggregate.State.ProjectId,
				command.JobTypeId,
				command.JobTypeName,
				command.UnitName,
				command.RatePerUnit,
				command.Amount
			);

			return Task.FromResult<EventData[]>(new[] { eventData });
		}
	}
}
