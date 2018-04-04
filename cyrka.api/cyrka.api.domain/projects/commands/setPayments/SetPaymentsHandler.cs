using System.Threading.Tasks;

namespace cyrka.api.domain.projects.commands.setPayments
{
	public class SetPaymentsHandler
	{
		public SetPaymentsHandler(ProjectAggregateRepository repository)
		{
			_repository = repository;
		}

		public async Task<PaymentsSet> Handle(string projectId, SetPayments command)
		{
			var project = await _repository.GetById(projectId);
			if (project == null)
				return null;
			return new PaymentsSet(project.State.ProjectId, command.TranslatorPayment, command.EditorPayment);
		}

		private readonly ProjectAggregateRepository _repository;
	}
}
