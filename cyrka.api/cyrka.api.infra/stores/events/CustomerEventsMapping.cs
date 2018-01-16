using cyrka.api.domain.customers;
using cyrka.api.domain.customers.commands.register;
using cyrka.api.domain.customers.commands.registerTitle;
using MongoDB.Bson.Serialization;

namespace cyrka.api.infra.stores.events
{
	public class CustomerEventsMapping : IDbMapping
	{
		public void DefineMaps()
		{
			BsonClassMap.RegisterClassMap<CustomerEventData>(cm =>
			{
				cm.MapField(cr => cr.CustomerId);
			});

			BsonClassMap.RegisterClassMap<CustomerRegistered>(cm =>
			{
				cm.MapField(cr => cr.Name);
				cm.MapField(cr => cr.Description);
				cm.MapCreator(cr => new CustomerRegistered(cr.CustomerId, cr.Name, cr.Description));
			});

			BsonClassMap.RegisterClassMap<TitleRegistered>(cm =>
			{
				cm.MapField(cr => cr.TitleId);
				cm.MapField(cr => cr.Name);
				cm.MapField(cr => cr.NumberOfSeries);
				cm.MapField(cr => cr.Description);
				cm.MapCreator(cr => new TitleRegistered(cr.CustomerId, cr.TitleId, cr.Name, cr.NumberOfSeries, cr.Description));
			});
		}
	}
}
