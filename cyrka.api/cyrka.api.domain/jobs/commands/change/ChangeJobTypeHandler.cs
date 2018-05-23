using System.Threading.Tasks;
using cyrka.api.common.commands;
using cyrka.api.common.events;

namespace cyrka.api.domain.jobs.commands.change
{
	public class ChangeJobTypeHandler : IAggregateCommandHandler<ChangeJobType, JobTypeAggregate>
	{
		public Task<EventData[]> Handle(ChangeJobType command, JobTypeAggregate aggregate)
		{
			var eventData = new JobTypeChanged(command.JobTypeId, command.Name, command.Description, command.Unit, command.Rate);

			return Task.FromResult<EventData[]>(new[] { eventData });
		}
	}
}
