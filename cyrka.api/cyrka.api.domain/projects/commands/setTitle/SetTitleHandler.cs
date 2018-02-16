using System.Threading.Tasks;

namespace cyrka.api.domain.projects.commands.setTitle
{
	public class SetTitleHandler
	{
		public SetTitleHandler(ProjectAggregateRepository repository)
		{
			_repository = repository;
		}

		public async Task<TitleSet> Handle(SetTitle command)
		{
			var project = await _repository.GetById(command.ProjectId);
			if (project == null)
				return null;
			return new TitleSet(project.State.ProjectId, command.TitleId, command.TitleName, command.NumberOfEpisodes);
		}

		private readonly ProjectAggregateRepository _repository;
	}
}
