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

		string[] aggregateTypes;
		IAggregateEventFactory[] aggEventFactories;

		[SetUp]
		public void Setup()
		{
			aggregateTypes = Enumerable
				.Range(0, TestContext.CurrentContext.Random.Next(20))
				.Select(i => TestContext.CurrentContext.Random.GetString())
				.ToArray();
			aggEventFactories = aggregateTypes
				.Select(s =>
				{
					var factory = A.Fake<IAggregateEventFactory>();
					A.CallTo(() => factory.AggregateType)
						.Returns(s);
					return factory;
				})
				.ToArray();
			sut = new EventFactory(A.Fake<IEventDataSerializer>(), aggEventFactories);
		}

		[Test]
		public void CreateEventFromDtoByAggregateEventFactory()
		{
			var ind = TestContext.CurrentContext.Random.Next(aggregateTypes.Length);
			var aggType = aggregateTypes[ind];
			var dto = new EventDto(
				1,
				DateTime.UtcNow,
				"someEvent",
				aggType,
				"someId",
				"data"
			);


			sut.Create(dto);

			A.CallTo(() => aggEventFactories[ind].Create(dto, A<IEventDataSerializer>.Ignored))
				.MustHaveHappened();
		}

		[Test]
		public void NotCreateEventFromDtoIfAggregateTypeIsUnknown()
		{
			var aggType = TestContext.CurrentContext.Random.GetString();
			var dto = new EventDto(
				1,
				DateTime.UtcNow,
				"someEvent",
				aggType,
				"someId",
				"data"
			);

			sut.Create(dto);

			foreach (var aggFactory in aggEventFactories)
			{
				A.CallTo(() => aggFactory.Create(dto, A<IEventDataSerializer>.Ignored))
					.MustNotHaveHappened();
			}
		}
	}
}
