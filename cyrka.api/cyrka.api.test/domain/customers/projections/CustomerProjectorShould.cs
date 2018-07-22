using System.Linq;
using System.Threading.Tasks;
using cyrka.api.common.events;
using cyrka.api.common.projections;
using cyrka.api.domain.customers.commands.register;
using cyrka.api.domain.customers.projections;
using cyrka.api.domain.customers.projections.viewModels;
using FakeItEasy;
using NUnit.Framework;

namespace cyrka.api.test.domain.customers.projections
{
	[TestFixture]
	public class CustomerProjectorShould
	{
		CustomerProjector _projectorUnderTest;

		private IProjectionStore<CustomerFullView> _projectionStore;
		private IProjectionOfEvent<CustomerFullView> _targetProjectionOfEvent;

		[SetUp]
		public void Setup()
		{
			_projectionStore = A.Fake<IProjectionStore<CustomerFullView>>();
			var projectionsOfEvent = Enumerable.Range(0, 100)
				.Select(_ => A.Fake<IProjectionOfEvent<CustomerFullView>>())
				.ToList();
			_targetProjectionOfEvent = projectionsOfEvent[
				TestContext.CurrentContext.Random.Next(0, projectionsOfEvent.Count)
			];

			_projectorUnderTest = new CustomerProjector(
				_projectionStore,
				projectionsOfEvent
			);
		}

		[Test]
		public async Task ProjectResultWithAppropriateEventProjection()
		{
			var eventToApply = A.Fake<Event>();
			A.CallTo(() => _targetProjectionOfEvent.CanProject(A<EventData>.Ignored))
				.Returns(true);

			await _projectorUnderTest.Apply(eventToApply);

			A.CallTo(() => _targetProjectionOfEvent.Project(A<EventData>.Ignored, A<CustomerFullView>.Ignored))
				.MustHaveHappenedOnceExactly();
		}
	}
}
