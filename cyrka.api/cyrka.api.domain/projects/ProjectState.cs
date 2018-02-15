using cyrka.api.common.identities;

namespace cyrka.api.domain.projects
{
	public class ProjectState
	{
		public CompositeSerialId ProjectId { get; set; }

		public (string Id, string Name) Customer { get; set; }

		public (string Id, string Name) Title { get; set; }

		public int EpisodeNumber { get; set; }
	}
}
