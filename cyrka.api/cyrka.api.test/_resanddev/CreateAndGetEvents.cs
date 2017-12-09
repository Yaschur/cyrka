using System;
using System.Threading.Tasks;
using cyrka.api.domain.customers.register;
using cyrka.api.domain.events;
using cyrka.api.infra.nexter;
using cyrka.api.infra.stores;
using MongoDB.Driver;
using NUnit.Framework;

namespace cyrka.api.test._resanddev
{
	[TestFixture, Explicit]
	public class CreateAndGetEvents
	{

		[Test]
		public async Task DoIt()
		{
			var client = new MongoClient();
			var mongoDb = client.GetDatabase("test");
			var nexterGenerator = new NexterGenerator();
			var eventsStore = new MongoEventStore(mongoDb);
			var commandHandler = new CustomerRegisterHandler();
			var eventDatas = commandHandler.Handle(new CustomerRegister("HBO", "Very very important customer. Use with care!"));
			var id = await nexterGenerator.GetNextNumber("events", await eventsStore.GetLastStoredId());
			var created = DateTime.UtcNow;

			await eventsStore.Store(new Event(id, created, eventDatas[0]));

			var restoredEvents = await eventsStore.FindAllAfterId(id - 1);

			Assert.AreEqual(1, restoredEvents.Length);
			Assert.IsInstanceOf<CustomerRegistered>(restoredEvents[0].EventData);
		}
	}
}
