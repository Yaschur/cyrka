namespace cyrka.api.domain.customers.commands.changeTitle
{
	public class ChangeTitle
	{
		public readonly string CustomerId;
		public readonly string TitleId;
		public readonly string Name;
		public readonly int NumberOfSeries;
		public readonly string Description;

		public ChangeTitle(string customerId, string titleId, string name, int numberOfSeries, string description)
		{
			CustomerId = customerId;
			TitleId = titleId;
			Name = name;
			NumberOfSeries = numberOfSeries;
			Description = description;
		}
	}
}
