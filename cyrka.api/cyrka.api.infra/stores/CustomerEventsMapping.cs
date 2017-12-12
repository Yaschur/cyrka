using cyrka.api.domain.customers.register;
using cyrka.api.domain.events;
using MongoDB.Bson.Serialization;

namespace cyrka.api.infra.stores
{
	public class CustomerEventsMapping : IDbMapping
	{
		public void DefineMaps()
		{
			BsonClassMap.RegisterClassMap<CustomerRegistered>(cm =>
			{
				cm.MapField(cr => cr.Name);
				cm.MapField(cr => cr.Description);
				cm.MapCreator(cr => new CustomerRegistered(cr.Name, cr.Description));
			});
		}
	}
}
