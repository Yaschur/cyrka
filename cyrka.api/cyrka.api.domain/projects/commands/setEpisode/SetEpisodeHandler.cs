using System.Threading.Tasks;

namespace cyrka.api.domain.projects.commands.setEpisode
{
	public class SetEpisodeHandler
	{
		public SetEpisodeHandler(ProjectAggregateRepository repository)
		{
			_repository = repository;
		}

		public async Task<EpisodeSet> Handle(SetEpisode command)
		{
			var project = await _repository.GetById(command.ProjectId);
			if (project == null)
				return null;
			return new EpisodeSet(project.State.ProjectId, command.Number, command.Duration);
		}

		private readonly ProjectAggregateRepository _repository;
	}
}
