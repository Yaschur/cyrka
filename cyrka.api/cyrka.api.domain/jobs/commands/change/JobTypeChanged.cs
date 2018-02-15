namespace cyrka.api.domain.jobs.commands.change
{
	public class JobTypeChanged : JobTypeEventData
	{
		public readonly string Name;
		public readonly string Description;
		public readonly JobTypeUnit Unit;
		public readonly decimal Rate;

		public JobTypeChanged(string id, string name, string description, JobTypeUnit unit, decimal rate)
			: base(id)
		{
			Name = name;
			Description = description;
			Unit = unit;
			Rate = rate;
		}
	}
}
