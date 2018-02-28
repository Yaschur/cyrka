using System.Linq;
using System.Threading.Tasks;

namespace cyrka.api.domain.projects.commands.changeJob
{
	public class ChangeJobHandler
	{
		public ChangeJobHandler(ProjectAggregateRepository repository)
		{
			_repository = repository;
		}

		public async Task<JobChanged> Handle(string projectId, ChangeJob command)
		{
			var project = await _repository.GetById(projectId);
			if (project == null)
				return null;
			var job = project.State.Jobs
				.FirstOrDefault(j => j.JobTypeId == command.JobTypeId);
			if (job == null)
				return null;

			return new JobChanged(
				project.State.ProjectId,
				job.JobTypeId,
				command.RatePerUnit,
				command.Amount
			);
		}

		private readonly ProjectAggregateRepository _repository;
	}
}
