namespace cyrka.api.domain.customers.commands.registerTitle
{
	public class TitleRegistered : CustomerEventData
	{
		public readonly string TitleId;
		public readonly string Name;
		public readonly int NumberOfSeries;
		public readonly string Description;

		public TitleRegistered(string customerId, string titleId, string name, int numberOfSeries, string description)
			: base(customerId)
		{
			TitleId = titleId;
			Name = name;
			NumberOfSeries = numberOfSeries;
			Description = description;
		}
	}
}
