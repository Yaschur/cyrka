namespace cyrka.api.domain.customers.commands.registerTitle
{
	public class RegisterTitle
	{
		public readonly string Name;
		public readonly int NumberOfSeries;
		public readonly string Description;

		public RegisterTitle(string name, int numberOfSeries, string description)
		{
			Name = name;
			NumberOfSeries = numberOfSeries;
			Description = description;
		}
	}
}
