using System.Threading.Tasks;
using cyrka.api.common.projections;
using cyrka.api.common.projections.results;
using FakeItEasy;
using NUnit.Framework;

namespace cyrka.api.test.common.projections.results
{
	[TestFixture]
	public class EmptyProjectionResultShould
	{
		IProjectionStore<IView> _store;

		[SetUp]
		public void Setup() => _store = A.Fake<IProjectionStore<IView>>();

		[Test]
		public async Task AccomplishByDoingNothing()
		{
			var view = A.Fake<IView>();
			var resultUnderTest = new EmptyProjectionResult<IView>();

			await resultUnderTest.AccomplishAsync(_store);

			A.CallTo(() => _store.StoreAsync(view))
				.MustNotHaveHappened();
			A.CallTo(() => _store.RemoveAsync(view))
				.MustNotHaveHappened();
		}
	}
}
