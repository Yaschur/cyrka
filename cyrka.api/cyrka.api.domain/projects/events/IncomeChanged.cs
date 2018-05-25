using cyrka.api.domain.projects.commands;

namespace cyrka.api.domain.projects.events
{
	public class IncomeChanged : ProjectEventData
	{
		public readonly decimal Addition;

		public IncomeChanged(string projectId, decimal addition)
			: base(projectId) => Addition = addition;
	}
}
