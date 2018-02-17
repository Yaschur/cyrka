namespace cyrka.api.domain.projects.commands.setEpisode
{
	public class SetEpisode
	{
		public readonly string ProjectId;
		public readonly int Number;
		public readonly int Duration;

		public SetEpisode(string projectId, int number, int duration)
		{
			ProjectId = projectId;
			Number = number;
			Duration = duration;
		}
	}
}
