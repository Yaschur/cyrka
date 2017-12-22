using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Subjects;
using System.Threading.Tasks;
using cyrka.api.common.events;
using cyrka.api.common.queries;
using FakeItEasy;
using NUnit.Framework;

namespace cyrka.api.test.common
{
	[TestFixture]
	public class QueryEventProcessorShould
	{
		[Test]
		public async Task DispatchEventsByRegister()
		{
			var eventObservable = new Subject<Event>();
			var eventStore = A.Fake<IEventStore>();
			A.CallTo(() => eventStore.AsObservable())
				.Returns(eventObservable);
			var queryStore = A.Fake<IQueryStore>();
			A.CallTo(() => queryStore.AsQueryable<object>())
				.Returns(Enumerable.Empty<object>().AsQueryable());

			var eventsProcessor = new QueryEventProcessor(eventStore, queryStore);
			var (event1Counter, event2Counter) = (0, 0);
			eventsProcessor.RegisterEventProcessing<EventData1, object>(
				(ed, o) => { event1Counter++; return new object(); },
				ed => o => true
			);
			eventsProcessor.RegisterEventProcessing<EventData1, object>(
				(ed, o) => { event1Counter++; return new object(); },
				ed => o => true
			);
			eventsProcessor.RegisterEventProcessing<EventData2, object>(
				(ed, o) => { event2Counter++; return new object(); },
				ed => o => true
			);
			var events = new List<Event> {
				new Event(0, DateTime.UtcNow, new EventData2()),
				new Event(0, DateTime.UtcNow, new EventData1()),
				new Event(0, DateTime.UtcNow, new EventData3()),
				new Event(0, DateTime.UtcNow, new EventData1()),
				new Event(0, DateTime.UtcNow, new EventData1()),
				new Event(0, DateTime.UtcNow, new EventData3()),
				new Event(0, DateTime.UtcNow, new EventData2()),
			};
			events.ForEach(e => eventObservable.OnNext(e));
			await Task.Delay(500);

			Assert.AreEqual(6, event1Counter);
			Assert.AreEqual(2, event2Counter);
		}
	}

	class EventData1 : EventData { }
	class EventData2 : EventData { }
	class EventData3 : EventData { }
}
