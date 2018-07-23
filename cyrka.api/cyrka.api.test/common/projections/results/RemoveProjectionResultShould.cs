using System.Threading.Tasks;
using cyrka.api.common.projections;
using cyrka.api.common.projections.results;
using FakeItEasy;
using NUnit.Framework;

namespace cyrka.api.test.common.projections.results
{
	[TestFixture]
	public class RemoveProjectionResultShould
	{
		IProjectionStore<IView> _store;

		[SetUp]
		public void Setup()
		{
			_store = A.Fake<IProjectionStore<IView>>();
		}

		[Test]
		public async Task AccomplishByRemovingItsArgument()
		{
			var view = A.Fake<IView>();
			var resultUnderTest = new RemoveProjectionResult<IView>(view);

			await resultUnderTest.AccomplishAsync(_store);

			A.CallTo(() => _store.RemoveAsync(view))
				.MustHaveHappenedOnceExactly();
		}
	}
}
