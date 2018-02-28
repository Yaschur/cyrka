using System.Threading.Tasks;

namespace cyrka.api.domain.projects.commands.setJob
{
	public class SetJobHandler
	{
		public SetJobHandler(ProjectAggregateRepository repository)
		{
			_repository = repository;
		}

		public async Task<JobSet> Handle(SetJob command)
		{
			var project = await _repository.GetById(command.ProjectId);
			if (project == null)
				return null;
			return new JobSet(
				project.State.ProjectId,
				command.JobTypeId,
				command.JobTypeName,
				command.UnitName,
				command.RatePerUnit,
				command.Amount
			);
		}

		private readonly ProjectAggregateRepository _repository;
	}
}
