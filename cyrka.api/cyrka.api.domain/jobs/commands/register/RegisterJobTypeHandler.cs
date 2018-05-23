using System;
using System.Threading.Tasks;
using cyrka.api.common.commands;
using cyrka.api.common.events;

namespace cyrka.api.domain.jobs.commands.register
{
	public class RegisterJobTypeHandler : IAggregateCommandHandler<RegisterJobType, JobTypeAggregate>
	{
		public Task<EventData[]> Handle(RegisterJobType command, JobTypeAggregate aggregate)
		{
			var id = Guid.NewGuid().ToString();
			var eventData = new JobTypeRegistered(id, command.Name, command.Description, command.Unit, command.Rate);

			return Task.FromResult<EventData[]>(new[] { eventData });
		}
	}
}
