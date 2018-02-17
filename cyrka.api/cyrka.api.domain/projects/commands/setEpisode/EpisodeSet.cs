namespace cyrka.api.domain.projects.commands.setEpisode
{
	public class EpisodeSet : ProjectEventData
	{
		public readonly int Number;
		public readonly int Duration;

		public EpisodeSet(string projectId, int episodeNumber, int episodeDuration)
			: base(projectId)
		{
			Number = episodeNumber;
			Duration = episodeDuration;
		}
	}
}
