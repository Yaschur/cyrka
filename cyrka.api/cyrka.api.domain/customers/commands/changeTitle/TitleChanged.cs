using cyrka.api.common.events;

namespace cyrka.api.domain.customers.commands.changeTitle
{
	public class TitleChanged : CustomerEventData
	{
		public readonly string TitleId;
		public readonly string Name;
		public readonly int NumberOfSeries;
		public readonly string Description;

		public TitleChanged(string customerId, string titleId, string name, int numberOfSeries, string description)
			: base(customerId)
		{
			TitleId = titleId;
			Name = name;
			NumberOfSeries = numberOfSeries;
			Description = description;
		}
	}
}
