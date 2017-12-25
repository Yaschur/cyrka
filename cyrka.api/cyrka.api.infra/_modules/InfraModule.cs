using System.Collections.Generic;
using Autofac;
using cyrka.api.common.events;
using cyrka.api.common.queries;
using cyrka.api.infra.nexter;
using cyrka.api.infra.stores;
using cyrka.api.infra.stores.events;
using cyrka.api.infra.stores.queries;
using MongoDB.Driver;

namespace cyrka.api.infra._modules
{
	public class InfraModule : Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			builder
				.RegisterType<NexterGenerator>()
				.SingleInstance();

			builder
				.Register<MongoQueryStore>(cc =>
				{
					var client = new MongoClient();
					var db = client.GetDatabase("cyrka_read");
					return new MongoQueryStore(db);
				})
				.As<IQueryStore>()
				.InstancePerRequest();

			// db mappers
			builder
				.RegisterType<CoreEventsMapping>()
				.As<IDbMapping>();
			builder
				.RegisterType<CustomerEventsMapping>()
				.As<IDbMapping>();

			builder
				.Register<MongoEventStore>(cc =>
				{
					var client = new MongoClient();
					var db = client.GetDatabase("cyrka_write");
					return new MongoEventStore(db, cc.Resolve<IEnumerable<IDbMapping>>());
				})
				.As<IEventStore>()
				.SingleInstance();
		}
	}
}
