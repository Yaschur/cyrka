namespace cyrka.api.domain.projects.commands.setTitle
{
	public class TitleSet : ProjectEventData
	{
		public readonly string TitleId;
		public readonly string TitleName;
		public readonly int NumberOfEpisodes;

		public TitleSet(string projectId, string titleId, string titleName, int numberOfEpisodes)
			: base(projectId)
		{
			TitleId = titleId;
			TitleName = titleName;
			NumberOfEpisodes = numberOfEpisodes;
		}
	}
}
