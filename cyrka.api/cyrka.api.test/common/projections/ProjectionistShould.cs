using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cyrka.api.common.events;
using cyrka.api.common.projections;
using FakeItEasy;
using NUnit.Framework;

namespace cyrka.api.test.common.projections
{
	[TestFixture]
	public class ProjectionistShould
	{
		Projectionist _projectionistUnderTest;

		List<IProjector> _projectors;
		IEventStore _eventStore;

		[SetUp]
		public void Setup()
		{
			_projectors = Enumerable.Range(0, TestContext.CurrentContext.Random.Next(10, 1000))
				.Select(_ => A.Fake<IProjector>())
				.ToList();
			_eventStore = A.Fake<IEventStore>();

			_projectionistUnderTest = new Projectionist(_eventStore, _projectors);
		}

		[Test]
		public async Task SubscribeToEventObservableStreamOnStart()
		{
			var eventObservable = A.Fake<IObservable<Event>>();
			A.CallTo(() => _eventStore.AsObservable(A<bool>.Ignored))
				.Returns(eventObservable);

			await _projectionistUnderTest.Start();

			A.CallTo(() => _eventStore.AsObservable(A<bool>.Ignored))
				.MustHaveHappenedOnceExactly();
			A.CallTo(() => eventObservable.Subscribe(A<IObserver<Event>>.Ignored))
				.MustHaveHappenedOnceExactly();
		}

		// [Test]
		// public async Task ApplyIncomingEventToAllItsProjectors()
		// {
		// 	var projectors = Enumerable.Range(0, TestContext.CurrentContext.Random.Next(10, 1000))
		// 		.Select(_ => A.Fake<IProjector>())
		// 		.ToList();
		// 	var incomingEvent = A.Fake<Event>();
		// 	var projectionistUnderTest = new Projectionist(projectors);

		// 	await projectionistUnderTest.Apply(incomingEvent);

		// 	projectors.ForEach(p => A.CallTo(() => p.Apply(incomingEvent))
		// 		.MustHaveHappenedOnceExactly()
		// 	);
		// }
	}
}
