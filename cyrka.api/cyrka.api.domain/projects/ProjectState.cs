using cyrka.api.common.identities;

namespace cyrka.api.domain.projects
{
	public class ProjectState
	{
		public CompositeSerialId ProjectId { get; set; }

		public ProjectStatus Status { get; set; } = ProjectStatus.Draft;

		public ProductState Product { get; set; }

		public JobState[] Jobs { get; set; } = { };
	}
}
