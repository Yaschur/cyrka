using cyrka.api.common.identities;

namespace cyrka.api.domain.projects
{
	public class ProjectState
	{
		public CompositeSerialId ProjectId { get; set; }

		public ProductState Product { get; set; }
	}
}
