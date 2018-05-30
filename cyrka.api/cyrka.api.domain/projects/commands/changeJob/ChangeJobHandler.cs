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
				var sum = aggregate.State.Jobs
					.Where(j => j.JobTypeId != command.JobTypeId)
					.Sum(j => j.Amount * j.RatePerUnit);
				var income = sum + command.Amount * command.RatePerUnit;

				if (aggregate.State.Money != null && aggregate.State.Money.Income == income)
					yield break;

				yield return new IncomeChanged(aggregate.State.ProjectId, income);
			}
		}
	}
}
