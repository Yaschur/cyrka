using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cyrka.api.common.commands;
using cyrka.api.common.events;
using cyrka.api.domain.projects.events;

namespace cyrka.api.domain.projects.commands.setPayments
{
	public class SetPaymentsHandler : IAggregateCommandHandler<SetPayments, ProjectAggregate>
	{
		public Task<EventData[]> Handle(SetPayments command, ProjectAggregate aggregate)
		{
			var eventData = new PaymentsSet(aggregate.State.ProjectId, command.TranslatorPayment, command.EditorPayment);

			return Task.FromResult(getEventData().ToArray());

			IEnumerable<EventData> getEventData()
			{
				yield return new PaymentsSet(aggregate.State.ProjectId, command.TranslatorPayment, command.EditorPayment);

				var oldSum = (aggregate.State.Payments?.EditorPayment ?? 0) + (aggregate.State.Payments?.TranslatorPayment ?? 0);
				var newSum = command.TranslatorPayment + command.EditorPayment;
				var addition = newSum - oldSum;
				if (addition == 0)
					yield break;

				yield return new IncomeChanged(
					aggregate.State.ProjectId,
					incomeAddition: 0,
					expensesAddition: addition
				);
			}
		}
	}
}
