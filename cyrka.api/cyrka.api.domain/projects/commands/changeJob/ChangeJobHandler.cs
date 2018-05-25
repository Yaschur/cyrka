using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cyrka.api.common.commands;
using cyrka.api.common.events;
using cyrka.api.domain.projects.errors;
using cyrka.api.domain.projects.events;

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

			return Task.FromResult(getEventData().ToArray());

			IEnumerable<EventData> getEventData()
			{
				yield return new JobChanged(
					aggregate.State.ProjectId,
					job.JobTypeId,
					command.RatePerUnit,
					command.Amount
				);
				var addition = command.Amount * command.RatePerUnit - job.Amount * job.RatePerUnit;

				if (addition == 0)
					yield break;

				yield return new IncomeChanged(
					aggregate.State.ProjectId,
					incomeAddition: addition,
					expensesAddition: 0
				);
			}
		}
	}
}
