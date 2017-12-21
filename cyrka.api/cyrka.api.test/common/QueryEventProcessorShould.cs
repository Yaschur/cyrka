using System.Reactive.Subjects;
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
		public void DispatchEventsByRegister()
		{
			var eventObservable = new Subject<Event>();
			var eventStore = A.Fake<IEventStore>();
			A.CallTo(() => eventStore.AsObservable())
				.Returns(eventObservable);
			var queryStore = A.Fake<IQueryStore>();

			// TODO...
		}
	}

	class EventData1 : EventData { }
	class EventData2 : EventData { }
	class EventData3 : EventData { }
}
