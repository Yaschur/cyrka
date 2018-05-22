using System.Threading.Tasks;
using cyrka.api.common.commands;
using cyrka.api.common.events;

namespace cyrka.api.domain.projects.commands.setPayments
{
	public class SetPaymentsHandler : IAggregateCommandHandler<SetPayments, ProjectAggregate>
	{
		public Task<EventData[]> Handle(SetPayments command, ProjectAggregate aggregate)
		{
			var eventData = new PaymentsSet(aggregate.State.ProjectId, command.TranslatorPayment, command.EditorPayment);

			return Task.FromResult<EventData[]>(new[] { eventData });
		}
	}
}
