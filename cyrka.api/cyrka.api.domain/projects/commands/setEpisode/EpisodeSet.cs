namespace cyrka.api.domain.projects.commands.setEpisode
{
	public class EpisodeSet : ProjectEventData
	{
		public readonly int EpisodeNumber;

		public EpisodeSet(string projectId, int episodeNumber)
			: base(projectId)
		{
			EpisodeNumber = episodeNumber;
		}
	}
}
