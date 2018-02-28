using System.Threading.Tasks;

namespace cyrka.api.domain.projects.commands.setProduct
{
	public class SetProductHandler
	{
		public SetProductHandler(ProjectAggregateRepository repository)
		{
			_repository = repository;
		}

		public async Task<ProductSet> Handle(string projectId, SetProduct command)
		{
			var project = await _repository.GetById(projectId);
			if (project == null)
				return null;
			return new ProductSet(
				project.State.ProjectId,
				command.CustomerId,
				command.CustomerName,
				command.TitleId,
				command.TitleName,
				command.TotalEpisodes,
				command.EpisodeNumber,
				command.EpisodeDuration
			);
		}

		private readonly ProjectAggregateRepository _repository;
	}
}
