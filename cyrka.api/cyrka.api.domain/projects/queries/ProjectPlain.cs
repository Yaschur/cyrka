namespace cyrka.api.domain.projects.queries
{
	public class ProjectPlain
	{
		public string Id { get; set; }

		public IdName Customer { get; set; }

		public IdName Title { get; set; }
		public int EpisodeNumber { get; set; }
	}
}
