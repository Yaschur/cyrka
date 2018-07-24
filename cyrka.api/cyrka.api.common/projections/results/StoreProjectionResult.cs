using System.Threading.Tasks;

namespace cyrka.api.common.projections.results
{
	public class StoreProjectionResult<TView> : IProjectionResult<TView>
		where TView : IView
	{
		public StoreProjectionResult(TView viewResult) => _view = viewResult;

		public Task AccomplishAsync(IProjectionStore<TView> projectionStore) => projectionStore.StoreAsync(_view);

		private readonly TView _view;
	}
}
