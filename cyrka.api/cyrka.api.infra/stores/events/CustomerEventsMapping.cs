using cyrka.api.domain.customers.commands.register;
using cyrka.api.domain.events;
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
		}
	}
}
