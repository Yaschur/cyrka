namespace cyrka.api.domain.customers.commands.changeTitle
{
	public class ChangeTitle
	{
		public readonly string TitleId;
		public readonly string Name;
		public readonly int NumberOfSeries;
		public readonly string Description;

		public ChangeTitle(string titleId, string name, int numberOfSeries, string description)
		{
			TitleId = titleId;
			Name = name;
			NumberOfSeries = numberOfSeries;
			Description = description;
		}
	}
}
