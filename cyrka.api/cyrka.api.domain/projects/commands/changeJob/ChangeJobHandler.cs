using System.Linq;
using System.Threading.Tasks;
using cyrka.api.common.commands;
using cyrka.api.common.events;
using cyrka.api.domain.projects.errors;

namespace cyrka.api.domain.projects.commands.changeJob
{
	public class ChangeJobHandler : IAggregateCommandHandler<ChangeJob, ProjectAggregate>
	{
		public Task<EventData[]> Handle(ChangeJob command, ProjectAggregate aggregate)
		{
			var job = aggregate.State.Jobs
				.FirstOrDefault(j => j.JobTypeId == command.JobTypeId);
			if (job == null)
				throw ProjectErrors.JobNotFoundError;

			var eventData = new JobChanged(
				aggregate.State.ProjectId,
				job.JobTypeId,
				command.RatePerUnit,
				command.Amount
			);

			return Task.FromResult<EventData[]>(new[] { eventData });
		}
	}
}
