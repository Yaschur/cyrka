using cyrka.api.domain.jobs;

namespace cyrka.api.web.models.jobs
{
	public class JobTypeInfo
	{
		public string Name { get; set; }
		public JobTypeUnit Unit { get; set; }
		public decimal Rate { get; set; }
		public string Description { get; set; }
	}
}
