using System;
using System.Collections.Generic;
using Autofac;
using cyrka.api.common.events;
using cyrka.api.common.queries;
using cyrka.api.infra.nexter;
using cyrka.api.infra.stores;
using cyrka.api.infra.stores.events;
using cyrka.api.infra.stores.queries;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;

namespace cyrka.api.infra._modules
{
	public class InfraModule : Module
	{
		const string WriteMongoDbKey = "write";
		const string ReadMongoDbKey = "read";

		public InfraModule(MongoDbConfiguration writeConfig, MongoDbConfiguration readConfig)
		{
			_readDbConfig = readConfig;
			_writeDbConfig = writeConfig;

			// TODO: move somewhere from di module
			var pack = new ConventionPack();
			pack.Add(new EnumRepresentationConvention(BsonType.String));
			ConventionRegistry.Register("EnumStringConvention", pack, t => true);
		}

		protected override void Load(ContainerBuilder builder)
		{
			builder
				.RegisterType<NexterGenerator>()
				.SingleInstance();

			// Configure MongoDb clients
			builder
				.Register<IMongoClient>(cc => new MongoClient(_writeDbConfig.ConnectionString))
				.Named<IMongoClient>(WriteMongoDbKey)
				.SingleInstance();
			builder
				.Register<IMongoClient>(cc =>
					_writeDbConfig.ConnectionString.Equals(_readDbConfig.ConnectionString, StringComparison.OrdinalIgnoreCase) ?
						cc.ResolveNamed<IMongoClient>(WriteMongoDbKey)
						: new MongoClient(_readDbConfig.ConnectionString)
				)
				.Named<IMongoClient>(ReadMongoDbKey)
				.SingleInstance();

			// Configure MongoDb databases
			builder
				.Register<IMongoDatabase>(cc =>
					cc.ResolveNamed<IMongoClient>(WriteMongoDbKey)
						.GetDatabase(_writeDbConfig.DatabaseName)
				)
				.Named<IMongoDatabase>(WriteMongoDbKey)
				.SingleInstance();
			builder
				.Register<IMongoDatabase>(cc =>
					cc.ResolveNamed<IMongoClient>(ReadMongoDbKey)
						.GetDatabase(_readDbConfig.DatabaseName)
				)
				.Named<IMongoDatabase>(ReadMongoDbKey)
				.SingleInstance();

			// Configure cyrkas' services

			builder
				.Register<MongoQueryStore>(cc => new MongoQueryStore(cc.ResolveNamed<IMongoDatabase>(ReadMongoDbKey)))
				.As<IQueryStore>()
				.SingleInstance();

			// db mappers
			builder
				.RegisterType<CoreEventsMapping>()
				.As<IDbMapping>();
			builder
				.RegisterType<ProjectEventsMapping>()
				.As<IDbMapping>();
			builder
				.RegisterType<JobTypeEventsMapping>()
				.As<IDbMapping>();

			builder
				.Register<MongoEventStore>(cc =>
					new MongoEventStore(
						cc.ResolveNamed<IMongoDatabase>(WriteMongoDbKey),
						cc.Resolve<IEnumerable<IDbMapping>>()
					)
				)
				.As<IEventStore>()
				.SingleInstance();
		}

		private readonly MongoDbConfiguration _writeDbConfig;
		private readonly MongoDbConfiguration _readDbConfig;
	}
}
