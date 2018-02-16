namespace cyrka.api.domain.projects.queries
{
	public class ProjectPlain
	{
		public string Id { get; set; }

		public ProjectCustomer Customer { get; set; }
		public ProjectTitle Title { get; set; }
		public int EpisodeNumber { get; set; }
	}
}
