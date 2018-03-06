using System.Threading.Tasks;

namespace cyrka.api.domain.projects.commands.setStatus
{
	public class SetStatusHandler
	{
		public SetStatusHandler(ProjectAggregateRepository repository)
		{
			_repository = repository;
		}

		public async Task<StatusSet> Handle(string projectId, SetStatus command)
		{
			var project = await _repository.GetById(projectId);
			if (project == null)
				return null;

			return new StatusSet(project.State.ProjectId, command.Status);
		}

		private readonly ProjectAggregateRepository _repository;
	}
}
