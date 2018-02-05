namespace cyrka.api.domain.jobs.queries
{
	public class JobTypePlain
	{
		public string Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public JobTypeUnit Unit { get; set; } = JobTypeUnit.Undefined;
		public decimal Rate { get; set; }
	}
}
