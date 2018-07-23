using System.Threading.Tasks;

namespace cyrka.api.common.projections.results
{
	public class RemoveProjectionResult<TView> : IProjectionResult<TView>
		where TView : IView
	{
		public RemoveProjectionResult(TView viewResult)
		{
			_view = viewResult;
		}

		public Task AccomplishAsync(IProjectionStore<TView> projectionStore)
		{
			return projectionStore.RemoveAsync(_view);
		}

		private readonly TView _view;
	}
}
