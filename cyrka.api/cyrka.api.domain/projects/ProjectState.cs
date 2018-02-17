using cyrka.api.common.identities;

namespace cyrka.api.domain.projects
{
	public class ProjectState
	{
		public CompositeSerialId ProjectId { get; set; }

		public ProjectCustomer Customer { get; set; }

		public ProjectTitle Title { get; set; }

		public ProjectEpisode Episode { get; set; }
	}
}
