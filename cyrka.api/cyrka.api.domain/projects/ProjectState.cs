namespace cyrka.api.domain.projects
{
	public class ProjectState
	{
		public string ProjectId { get; set; }

		public (string Id, string Name) Customer { get; set; }

		public (string Id, string Name) Title { get; set; }

		public int EpisodeNumber { get; set; }
	}
}
