using cyrka.api.common.events;

namespace cyrka.api.domain.customers.commands.registerTitle
{
	public class TitleRegistered : EventData
	{
		public readonly string CustomerId;
		public readonly string Id;
		public readonly string Name;
		public readonly int NumberOfSeries;
		public readonly string Description;

		public TitleRegistered(string customerId, string id, string name, int numberOfSeries, string description)
		{
			CustomerId = customerId;
			Id = id;
			Name = name;
			NumberOfSeries = numberOfSeries;
			Description = description;
		}
	}
}
