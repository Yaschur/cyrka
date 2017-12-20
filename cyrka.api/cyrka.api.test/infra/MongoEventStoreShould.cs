using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using cyrka.api.common.events;
using cyrka.api.infra.stores;
using cyrka.api.infra.stores.events;
using FakeItEasy;
using MongoDB.Driver;
using NUnit.Framework;

namespace cyrka.api.test.infra
{
	[TestFixture]
	public class MongoEventStoreShould
	{
		[Test]
		public void ProvideEventsChannelOnStore()
		{
			var mongoDb = A.Fake<IMongoDatabase>();
			var dbMaps = A.CollectionOfDummy<IDbMapping>(1);
			var storeUnderTest = new MongoEventStore(mongoDb, dbMaps);
			var channel = storeUnderTest.AsObservable();
			var eventsCollector = new ConcurrentBag<Event>();
			var subs = channel.Subscribe(eventsCollector.Add);
			var events = new List<Event> {
				new Event(1, DateTime.Now, new EventData()),
				new Event(2, DateTime.Now, new EventData()),
				new Event(3, DateTime.Now, new EventData())
			};

			events.ForEach(async e => await storeUnderTest.Store(e));

			// TODO: this may fail if Store will send Event to remote Queue (latency)
			Assert.AreEqual(3, eventsCollector.Count);
			Assert.IsTrue(events.Any(e => e.Id == 2));
			Assert.IsFalse(events.Any(e => e.Id == 5));
			subs.Dispose();
		}
	}
}
