using System.Threading.Tasks;
using cyrka.api.common.projections;
using cyrka.api.common.projections.results;
using FakeItEasy;
using NUnit.Framework;

namespace cyrka.api.test.common.projections.results
{
	[TestFixture]
	public class StoreProjectionResultShould
	{
		IProjectionStore<IView> _store;

		[SetUp]
		public void Setup() => _store = A.Fake<IProjectionStore<IView>>();

		[Test]
		public async Task AccomplishByStoringItsArgument()
		{
			var view = A.Fake<IView>();
			var resultUnderTest = new StoreProjectionResult<IView>(view);

			await resultUnderTest.AccomplishAsync(_store);

			A.CallTo(() => _store.StoreAsync(view))
				.MustHaveHappenedOnceExactly();
		}
	}
}
