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

				var expenses = command.TranslatorPayment + command.EditorPayment;
				if (aggregate.State.Money != null && expenses == aggregate.State.Money.Expenses)
					yield break;

				yield return new IncomeChanged(aggregate.State.ProjectId, expenses, isExpenses: true);
			}
		}
	}
}
