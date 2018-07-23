using System.Threading.Tasks;

namespace cyrka.api.common.projections.results
{
	public class EmptyProjectionResult<TView> : IProjectionResult<TView>
		where TView : IView
	{
		public Task AccomplishAsync(IProjectionStore<TView> projectionStore) => Task.CompletedTask;
	}
}
