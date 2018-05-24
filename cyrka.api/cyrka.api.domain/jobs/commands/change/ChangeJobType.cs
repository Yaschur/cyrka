namespace cyrka.api.domain.jobs.commands.change
{
	public class ChangeJobType
	{
		public readonly string Name;
		public readonly string Description;
		public readonly JobTypeUnit Unit;
		public readonly decimal Rate;

		public ChangeJobType(string name, string description, JobTypeUnit unit, decimal rate)
		{
			Name = name;
			Description = description;
			Unit = unit;
			Rate = rate;
		}
	}
}
