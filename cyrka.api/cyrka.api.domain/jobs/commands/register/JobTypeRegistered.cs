namespace cyrka.api.domain.jobs.commands.register
{
	public class JobTypeRegistered : JobTypeEventData
	{
		public readonly string Name;
		public readonly string Description;
		public readonly JobTypeUnit Unit;
		public readonly decimal Rate;

		public JobTypeRegistered(string jobTypeId, string name, string description, JobTypeUnit unit, decimal rate)
			: base(jobTypeId)
		{
			Name = name;
			Description = description;
			Unit = unit;
			Rate = rate;
		}
	}
}
