using cyrka.api.domain.projects.commands;

namespace cyrka.api.domain.projects.events
{
	public class IncomeChanged : ProjectEventData
	{
		public readonly decimal IncomeAddition;
		public readonly decimal ExpensesAddition;

		public IncomeChanged(string projectId, decimal incomeAddition, decimal expensesAddition)
			: base(projectId) => (IncomeAddition, ExpensesAddition) = (incomeAddition, expensesAddition);
	}
}
