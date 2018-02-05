using System;
using cyrka.api.common.events;

namespace cyrka.api.domain.jobs.commands.change
{
	public class ChangeJobTypeHandler
	{
		public EventData[] Handle(ChangeJobType command)
		{
			var jobTypeChanged = new JobTypeChanged(command.JobTypeId, command.Name, command.Description, command.Unit, command.Rate);
			return new[] { jobTypeChanged };
		}
	}
}
