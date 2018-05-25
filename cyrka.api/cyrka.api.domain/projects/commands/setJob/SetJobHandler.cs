using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cyrka.api.common.commands;
using cyrka.api.common.events;
using cyrka.api.domain.projects.errors;
using cyrka.api.domain.projects.events;

namespace cyrka.api.domain.projects.commands.setJob
{
	public class SetJobHandler : IAggregateCommandHandler<SetJob, ProjectAggregate>
	{
		public Task<EventData[]> Handle(SetJob command, ProjectAggregate aggregate)
		{
			if (aggregate.State.Jobs.Any(j => j.JobTypeId == command.JobTypeId))
				throw ProjectErrors.JobNotFoundError;

			return Task.FromResult(getEventData().ToArray());

			IEnumerable<EventData> getEventData()
			{
				yield return new JobSet(
					aggregate.State.ProjectId,
					command.JobTypeId,
					command.JobTypeName,
					command.UnitName,
					command.RatePerUnit,
					command.Amount
				);
				var addition = command.Amount * command.RatePerUnit;

				if (addition == 0)
					yield break;

				yield return new IncomeChanged(
					aggregate.State.ProjectId,
					addition,
					0
				);
			}
		}
	}
}
