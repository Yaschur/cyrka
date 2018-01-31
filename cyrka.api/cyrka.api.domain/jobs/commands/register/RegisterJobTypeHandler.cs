using System;
using cyrka.api.common.events;

namespace cyrka.api.domain.jobs.commands.register
{
	public class RegisterJobTypeHandler
	{
		public EventData[] Handle(RegisterJobType command)
		{
			var id = Guid.NewGuid().ToString();
			var jobTypeRegistered = new JobTypeRegistered(id, command.Name, command.Description, command.Unit, command.Rate);
			return new[] { jobTypeRegistered };
		}
	}
}
