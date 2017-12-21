using System;
using System.Linq;
using System.Threading.Tasks;
using cyrka.api.common.events;
using cyrka.api.common.queries;
using cyrka.api.domain.customers.commands.register;
using cyrka.api.domain.customers.queries;
using cyrka.api.infra.nexter;
using cyrka.api.infra.stores;
using cyrka.api.infra.stores.events;
using cyrka.api.infra.stores.queries;
using MongoDB.Driver;
using NUnit.Framework;

namespace cyrka.api.test._resanddev
{
	[TestFixture, Explicit]
	public class CreateAndGetWithMongo
	{
		[Test]
		public async Task DoCommand()
		{
			var client = new MongoClient();
			var mongoDb = client.GetDatabase("test-command");
			var nexterGenerator = new NexterGenerator();
			var eventsStore = new MongoEventStore(
				mongoDb,
				new IDbMapping[] { new CoreEventsMapping(), new CustomerEventsMapping() }
			);
			var commandHandler = new CustomerRegisterHandler();
			var eventDatas = commandHandler.Handle(new CustomerRegister("HBO", "Very very important customer. Use with care!"));
			var id = await nexterGenerator.GetNextNumber("events", await eventsStore.GetLastStoredId());
			var created = DateTime.UtcNow;

			await eventsStore.Store(new Event(id, created, eventDatas[0]));

			var restoredEvents = await eventsStore.FindAllAfterId(id - 1);

			Assert.AreEqual(1, restoredEvents.Length);
			Assert.IsInstanceOf<CustomerRegistered>(restoredEvents[0].EventData);
		}

		[Test]
		public async Task DoQuery()
		{
			var client = new MongoClient();
			var mongoDb = client.GetDatabase("test-query");
			var queriesStore = new MongoQueryStore(mongoDb);

			var plainCustomer1 = new CustomerPlain { Id = "abc", Name = "abc" };
			await queriesStore.Upsert(plainCustomer1, pc => pc.Id == plainCustomer1.Id);

			var restored1 = queriesStore.AsQueryable<CustomerPlain>().ToList();

			var plainCustomer2 = new CustomerPlain { Id = "abc", Name = "xyz", Description = "abc xyz" };
			await queriesStore.Upsert(plainCustomer2, pc => pc.Id == plainCustomer2.Id);

			var restored2 = queriesStore.AsQueryable<CustomerPlain>().ToList();
		}

		[Test]
		public async Task DoSubscription()
		{
			var client = new MongoClient();
			var mongoDbCommand = client.GetDatabase("test-command");
			var nexterGenerator = new NexterGenerator();
			var eventsStore = new MongoEventStore(
				mongoDbCommand,
				new IDbMapping[] { new CoreEventsMapping(), new CustomerEventsMapping() }
			);

			var mongoDbQuery = client.GetDatabase("test-query");
			var queriesStore = new MongoQueryStore(mongoDbQuery);

			var queryEventProcessor = new QueryEventProcessor(eventsStore, queriesStore);
			queryEventProcessor.RegisterEventProcessing<CustomerRegistered, CustomerPlain>(
				(ed, c) => new CustomerPlain { Id = ed.Id, Name = ed.Name, Description = ed.Description },
				ed => c => c.Id == ed.Id
			);

			var commandHandler = new CustomerRegisterHandler();
			var eventDatas = commandHandler.Handle(new CustomerRegister("HBO", "Very very important customer. Use with care!"));
			var id = await nexterGenerator.GetNextNumber("events", await eventsStore.GetLastStoredId());
			var created = DateTime.UtcNow;

			await eventsStore.Store(new Event(id, created, eventDatas[0]));
		}
	}
}
