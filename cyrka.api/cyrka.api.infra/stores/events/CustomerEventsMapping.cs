using cyrka.api.domain.customers;
using cyrka.api.domain.customers.commands;
using cyrka.api.domain.customers.commands.change;
using cyrka.api.domain.customers.commands.changeTitle;
using cyrka.api.domain.customers.commands.register;
using cyrka.api.domain.customers.commands.registerTitle;
using cyrka.api.domain.customers.commands.removeTitle;
using cyrka.api.domain.customers.commands.retire;
using MongoDB.Bson.Serialization;

namespace cyrka.api.infra.stores.events
{
	public class CustomerEventsMapping : IDbMapping
	{
		public void DefineMaps()
		{
			BsonClassMap.RegisterClassMap<CustomerEventData>();

			BsonClassMap.RegisterClassMap<CustomerRegistered>(cm =>
			{
				cm.MapField(cr => cr.Name);
				cm.MapField(cr => cr.Description);
				cm.MapCreator(cr => new CustomerRegistered(cr.AggregateId, cr.Name, cr.Description));
			});

			BsonClassMap.RegisterClassMap<CustomerChanged>(cm =>
			{
				cm.MapField(cr => cr.Name);
				cm.MapField(cr => cr.Description);
				cm.MapCreator(cr => new CustomerChanged(cr.AggregateId, cr.Name, cr.Description));
			});

			BsonClassMap.RegisterClassMap<TitleRegistered>(cm =>
			{
				cm.MapField(cr => cr.TitleId);
				cm.MapField(cr => cr.Name);
				cm.MapField(cr => cr.NumberOfSeries);
				cm.MapField(cr => cr.Description);
				cm.MapCreator(cr => new TitleRegistered(cr.AggregateId, cr.TitleId, cr.Name, cr.NumberOfSeries, cr.Description));
			});

			BsonClassMap.RegisterClassMap<TitleChanged>(cm =>
			{
				cm.MapField(cr => cr.TitleId);
				cm.MapField(cr => cr.Name);
				cm.MapField(cr => cr.NumberOfSeries);
				cm.MapField(cr => cr.Description);
				cm.MapCreator(cr => new TitleChanged(cr.AggregateId, cr.TitleId, cr.Name, cr.NumberOfSeries, cr.Description));
			});

			BsonClassMap.RegisterClassMap<CustomerRetired>(cm =>
			{
				cm.MapCreator(cr => new CustomerRetired(cr.AggregateId));
			});

			BsonClassMap.RegisterClassMap<TitleRemoved>(cm =>
			{
				cm.MapField(cr => cr.TitleId);
				cm.MapCreator(cr => new TitleRemoved(cr.AggregateId, cr.TitleId));
			});
		}
	}
}
