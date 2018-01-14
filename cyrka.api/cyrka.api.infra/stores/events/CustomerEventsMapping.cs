using cyrka.api.domain.customers.commands.register;
using cyrka.api.domain.customers.commands.registerTitle;
using MongoDB.Bson.Serialization;

namespace cyrka.api.infra.stores.events
{
	public class CustomerEventsMapping : IDbMapping
	{
		public void DefineMaps()
		{
			BsonClassMap.RegisterClassMap<CustomerRegistered>(cm =>
			{
				cm.MapField(cr => cr.Id);
				cm.MapField(cr => cr.Name);
				cm.MapField(cr => cr.Description);
				cm.MapCreator(cr => new CustomerRegistered(cr.Id, cr.Name, cr.Description));
			});

			BsonClassMap.RegisterClassMap<TitleRegistered>(cm =>
			{
				cm.MapField(cr => cr.CustomerId);
				cm.MapField(cr => cr.Id);
				cm.MapField(cr => cr.Name);
				cm.MapField(cr => cr.NumberOfSeries);
				cm.MapField(cr => cr.Description);
				cm.MapCreator(cr => new TitleRegistered(cr.CustomerId, cr.Id, cr.Name, cr.NumberOfSeries, cr.Description));
			});
		}
	}
}
