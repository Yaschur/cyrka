using System.Threading.Tasks;

namespace cyrka.api.common.projections
{
	public interface IProjectionResult
	{
		Task AccomplishAsync<TView>(IProjectionStore<TView> projectionStore)
			where TView : IView;
	}
}
