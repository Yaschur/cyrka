namespace cyrka.api.domain.projects.queries
{
	public class ProjectPlain
	{
		public string Id { get; set; }

		public (string Id, string Name) Customer { get; set; }

		public (string Id, string Name) Title { get; set; }

		public int EpisodeNumber { get; set; }
	}
}
