using cyrka.api.domain.projects.commands;

namespace cyrka.api.domain.projects.events
{
	public class IncomeChanged : ProjectEventData
	{
		public readonly decimal Value;
		public readonly bool IsExpenses;

		public IncomeChanged(string projectId, decimal value, bool isExpenses = false)
			: base(projectId) => (Value, IsExpenses) = (value, isExpenses);
	}
}
