namespace cyrka.api.domain.projects.commands.setTitle
{
	public class SetTitle
	{
		public readonly string ProjectId;
		public readonly string TitleId;
		public readonly string TitleName;

		public readonly int NumberOfEpisodes;

		public SetTitle(string projectId, string titleId, string titleName, int numberOfEpisodes)
		{
			ProjectId = projectId;
			TitleId = titleId;
			TitleName = titleName;
			NumberOfEpisodes = numberOfEpisodes;
		}
	}
}
