using System;
using System.Linq;
using cyrka.api.domain.events;
using FakeItEasy;
using NUnit.Framework;

namespace cyrka.api.test.domain
{
	[TestFixture]
	public class EventFactoryShould
	{
		EventFactory sut;

		string[] eventTypes;
		IAggregateEventFactory[] aggEventFactories;

		[SetUp]
		public void Setup()
		{
			eventTypes = Enumerable
				.Range(0, TestContext.CurrentContext.Random.Next(20))
				.Select(i => TestContext.CurrentContext.Random.GetString())
				.ToArray();
			aggEventFactories = eventTypes
				.Select(s =>
				{
					var factory = A.Fake<IAggregateEventFactory>();
					A.CallTo(() => factory.AggregateType)
						.Returns(s);
					A.CallTo(() => factory.Create(A<EventDto>.Ignored, A<IEventDataSerializer>.Ignored))
						.Returns(A.Fake<Event>());
					return factory;
				})
				.ToArray();
			sut = new EventFactory(A.Fake<IEventDataSerializer>(), aggEventFactories);
		}

		[Test]
		public void CreateEvent()
		{
			var dto = new EventDto(
				1,
				DateTime.UtcNow,
				"someEvent",
				eventTypes[TestContext.CurrentContext.Random.Next(eventTypes.Length)],
				"someId",
				"data"
			);

			var result = sut.Create(dto);

			Assert.NotNull(result);
		}
	}
}
