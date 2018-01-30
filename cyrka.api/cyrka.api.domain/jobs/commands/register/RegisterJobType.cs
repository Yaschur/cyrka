namespace cyrka.api.domain.jobs.commands.register
{
	public class RegisterJobType
	{
		public readonly string Name;
		public readonly string Description;
		public readonly JobTypeUnit Unit;
		public readonly decimal Rate;

		public RegisterJobType(string name, string description, JobTypeUnit unit, decimal rate)
		{
			Name = name;
			Description = description;
			Unit = unit;
			Rate = rate;
		}
	}
}
